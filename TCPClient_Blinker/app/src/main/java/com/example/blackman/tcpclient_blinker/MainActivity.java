package com.example.blackman.tcpclient_blinker;

import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ScrollView;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

public class MainActivity extends AppCompatActivity {
    /** Called when the activity is first created. */
    public Socket cSocket = null;
    private String serverIP;         // 서버 ip주소
    private int port = 9000;         // 포트번호

    public PrintWriter streamOut = null;
    public BufferedReader streamIn = null;

    public chatThread cThread = null;

    public EditText serverIPText;
    public TextView textView;
    public ScrollView scrollView;
    public Button changeBrithnessBtn, revertBrightnessBtn;
    public Button showAlrimchangBtn, closeAlrimchangBtn;
    public Button showEyeEvent1Btn, showEyeEvent2Btn;

    public String nickName;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        initUI();       // UI 초기화
    }

    public void initUI(){
        serverIPText = (EditText)findViewById(R.id.serverIP_textBox);
        textView = (TextView)findViewById(R.id.txtView);
        scrollView = (ScrollView)findViewById(R.id.scrollView1);
        changeBrithnessBtn = (Button)findViewById(R.id.changeBrightBtn);
        revertBrightnessBtn = (Button)findViewById(R.id.revertBrightBtn);
        showAlrimchangBtn = (Button)findViewById(R.id.showAlrimchangBtn);
        closeAlrimchangBtn = (Button)findViewById(R.id.closeAlrimchangBtn);
        showEyeEvent1Btn = (Button)findViewById(R.id.eyeEvent1Btn);
        showEyeEvent2Btn = (Button)findViewById(R.id.eyeEvent2Btn);
    }

    public void onDestroy() { // 앱이 소멸되면
        super.onDestroy();
        if (cSocket != null) {
            sendMessage("# [" + nickName + "]님이 나가셨습니다.");
        }
    }

    public void connBtnClick(View v) {
        switch (v.getId()) {
            case R.id.connBtn: // 접속버튼
                if (cSocket == null) {
                    logger("접속중입니다...");
                    serverIP = serverIPText.getText().toString();
                    hideKeyBoard();  // 키보드 숨기기
                    connect(serverIP, port , "Android_Blinker_Client");
                }
                break;
            case R.id.closeBtn: // 나가기 버튼
                if (cSocket != null) {
                    sendMessage("# [" + nickName + "]님이 나가셨습니다.");
                }
                break;

        }
    }

    public void communicationBtnClick(View v){
        switch (v.getId())
        {
            case R.id.changeBrightBtn: // 화면 색상 바꾸는 버튼 클릭시
                sendMessageToServer(MagicNumber.CHANGE_BRIGHTNESS);
                break;
            case R.id.revertBrightBtn: // 화면 색상 원래대로 가게하는 버튼 클릭시
                sendMessageToServer(MagicNumber.REVERT_BRIGHTNESS);
                break;
            case R.id.showAlrimchangBtn: // 알림창 나오는 버튼 클릭시
                sendMessageToServer(MagicNumber.SHOW_ALRIMCHANG);
                break;
            case R.id.closeAlrimchangBtn: // 알림창 닫는 버튼 클릭시
                sendMessageToServer(MagicNumber.CLOSE_ALRIMCHANG);
                break;
            case R.id.eyeEvent1Btn: // 눈 운동 이벤트 1 버튼 클릭시
                sendMessageToServer(MagicNumber.SHOW_EYE_EVENT1);
                break;
            case R.id.eyeEvent2Btn: // 눈 운동 이벤트 2 버튼 클릭시
                sendMessageToServer(MagicNumber.SHOW_EYE_EVENT2);
                break;
        }
    }

    // 메세지 서버에 보내는 함수
    // 소켓이 비어있지 않으면 sendMessage() 함수를 통해 전송
    private void sendMessageToServer(String msgString)
    {
        if (cSocket != null) {
            if (msgString != null && !"".equals(msgString)) {
                sendMessage(msgString);
            }
        } else {
            logger("접속을 먼저 해주세요.");
        }
    }


    // 연결 설정하기
    private class connectTask extends AsyncTask<String, Void , Socket> {

        @Override
        protected Socket doInBackground(String... params) {
            try {
                cSocket = new Socket(serverIP, port);
                streamOut = new PrintWriter(cSocket.getOutputStream(), true);
                streamIn = new BufferedReader(new InputStreamReader(cSocket.getInputStream()));
            } catch (UnknownHostException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }

            sendMessage("# Blinker Clinet 접속 완료");

            // 다수 연결자를 위한 Thread 생성
            cThread = new chatThread();
            cThread.start();

            return null;
        }

    }


    public void connect(String server, int port, String user) {
        System.out.println("커넥트 시작");
        new connectTask().execute(server);

    }

    private void logger(String MSG) {
        textView.append(MSG + "\n");     // 텍스트뷰에 메세지를 더해줍니다.
        scrollView.fullScroll(ScrollView.FOCUS_DOWN); // 스크롤뷰의 스크롤을 내려줍니다.
    }

    private void sendMessage(String MSG) {
        try {
            streamOut.println(MSG);     // 서버에 메세지를 보내줍니다.
        } catch (Exception ex) {
            logger(ex.toString());
        }
    }

    Handler mHandler = new Handler() {   // 스레드에서 메세지를 받을 핸들러.
        public void handleMessage(Message msg) {
            switch (msg.what) {
                case 0: // 채팅 메세지를 받아온다.
                    logger(msg.obj.toString());
                    break;
                case 1: // 소켓접속을 끊는다.
                    try {
                        cSocket.close();
                        cSocket = null;

                        logger("접속이 끊어졌습니다.");

                    } catch (Exception e) {
                        logger("접속이 이미 끊겨 있습니다." + e.getMessage());
                        finish();
                    }
                    break;
            }
        }
    };
    public void hideKeyBoard(){
        InputMethodManager imm = (InputMethodManager)getSystemService( this.INPUT_METHOD_SERVICE );
        imm.hideSoftInputFromWindow( serverIPText.getWindowToken(), 0 );
    }

    class chatThread extends Thread {
        private boolean flag = false; // 스레드 유지(종료)용 플래그
        public void run() {
            try {
                while (!flag) { // 플래그가 false일경우에 루프
                    String msgs;
                    Message msg = new Message();
                    msg.what = 0;
                    msgs = streamIn.readLine();  // 서버에서 올 메세지를 기다린다.
                    msg.obj = msgs;

                    mHandler.sendMessage(msg); // 핸들러로 메세지 전송

                    if (msgs.equals("# [" + nickName + "]님이 나가셨습니다.")) { // 서버에서 온 메세지가 종료 메세지라면
                        flag = true;   // 스레드 종료를 위해 플래그를 true로 바꿈.
                        msg = new Message();
                        msg.what = 1;   // 종료메세지
                        mHandler.sendMessage(msg);
                    }
                }

            }catch(Exception e) {
                logger(e.getMessage());
            }
        }
    }
}