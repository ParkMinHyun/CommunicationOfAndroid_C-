package com.example.blackman.tcpclient_blinker;

import android.os.AsyncTask;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
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
    private String server = "172.30.1.19";         // 서버 ip주소
    private int port = 9000;                           // 포트번호

    public PrintWriter streamOut = null;
    public BufferedReader streamIn = null;

    public chatThread cThread = null;

    public EditText nickText;
    public Button changeBrithnessBtn, revertBrightnessBtn;
    public Button showAlrimchangBtn, closeAlrimchangBtn;
    public Button showEyeEvent1Btn, showEyeEvent2Btn;

    public String nickName;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        initUI();

        logger("채팅을 시작합니다.");
    }

    public void initUI(){
        nickText = (EditText)findViewById(R.id.connText);
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
                    nickName = nickText.getText().toString();
                    logger("접속중입니다...");
                    connect(server, port , nickName);
                }
                break;
            case R.id.closeBtn: // 나가기 버튼
                if (cSocket != null) {
                    sendMessage("# [" + nickName + "]님이 나가셨습니다.");
                }
                break;
            case R.id.changeBrightBtn: // 화면 색상 바꾸는 버튼 클릭시


                break;
        }
    }

    private void sendMessageToServer()
    {
        if (cSocket != null) {
            String msgString = "1";
            if (msgString != null && !"".equals(msgString)) {
                sendMessage("[" + nickName + "] " + msgString);
                msgText.setText("");
            }
        } else {
            logger("접속을 먼저 해주세요.");
        }
    }

    private class connectTask extends AsyncTask<String, Void , Socket> {

        @Override
        protected Socket doInBackground(String... params) {
// TODO Auto-generated method stub
            try {
                cSocket = new Socket(server, port);
                streamOut = new PrintWriter(cSocket.getOutputStream(), true);
                streamIn = new BufferedReader(new InputStreamReader(cSocket.getInputStream()));
            } catch (UnknownHostException e) {
// TODO Auto-generated catch block
                e.printStackTrace();
            } catch (IOException e) {
// TODO Auto-generated catch block
                e.printStackTrace();
            }

            sendMessage("# 새로운 이용자님이 들어왔습니다.");

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
        tv.append(MSG + "\n");     // 텍스트뷰에 메세지를 더해줍니다.
        sv.fullScroll(ScrollView.FOCUS_DOWN); // 스크롤뷰의 스크롤을 내려줍니다.
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