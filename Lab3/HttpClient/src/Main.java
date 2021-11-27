import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.InputStreamReader;
import java.io.LineNumberReader;
import java.net.URL;

public class Main {
    static JFrame frame;
    static JTextField textFieldAddress;
    static JTextArea textAreaHtml;
    static JButton buttonDownload;
    static JScrollPane scrollPane;

    public static String Load(String strURL){
        textAreaHtml.setText("Start loading...");
        String outString = "";
        try {
            URL url = new URL(strURL);
            try {
                LineNumberReader reader = new LineNumberReader(new InputStreamReader(url.openStream()));
                String string = reader.readLine();
                while (string != null) {
                    outString += string;
                    string = reader.readLine();
                }
                reader.close();
            }
            catch (Exception e) {
                textAreaHtml.setText(e.getMessage());
            }
        }
        catch (Exception ex) {
            textAreaHtml.setText(ex.getMessage());
        }
        return outString;
    }

    public static void runAndListen() {
        buttonDownload.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                textAreaHtml.setText(Load(textFieldAddress.getText()));
            }
        });
    }

    public static void main(String[] args) {
        frame = new JFrame("Http-Client");
        frame.setSize(800, 600);
        frame.setResizable(false);

        textFieldAddress = new JTextField("https://javarush.ru");
        textFieldAddress.setBounds(20, 20, 150, 20);
        textFieldAddress.setVisible(true);

        buttonDownload = new JButton("download");
        buttonDownload.setBounds(textFieldAddress.getX(), textFieldAddress.getY() + textFieldAddress.getHeight() + 10, 150, 30);
        buttonDownload.setVisible(true);

        textAreaHtml = new JTextArea("<>");
        textAreaHtml.setBounds(textFieldAddress.getX(), buttonDownload.getY() + buttonDownload.getHeight() + 10, 750, 400);
        textAreaHtml.setVisible(true);

        textAreaHtml.setLineWrap(true);
        textAreaHtml.setWrapStyleWord(true);

        scrollPane = new JScrollPane(textAreaHtml);
        scrollPane.setBounds(textFieldAddress.getX(), buttonDownload.getY() + buttonDownload.getHeight() + 10, 750, 400);
        scrollPane.setVisible(true);

        JPanel panel = new JPanel();
        panel.setLayout(null);
        panel.add(textFieldAddress);
        panel.add(buttonDownload);
        panel.add(scrollPane);

        frame.setContentPane(panel);
        frame.setVisible(true);
        frame.setDefaultCloseOperation(frame.EXIT_ON_CLOSE);

        javax.swing.SwingUtilities.invokeLater(new Runnable() {
            @Override
            public void run() {
                runAndListen();
            }
        });

    }
}