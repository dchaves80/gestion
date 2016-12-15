import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.JScrollPane;
import javax.swing.JLabel;
import javax.swing.ScrollPaneConstants;
import javax.swing.JInternalFrame;
import java.awt.GridLayout;
import javax.swing.JRadioButton;

public class MTSframe {

	
	
	private JFrame frame;

	public JInternalFrame IF;
	
	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					MTSframe window = new MTSframe();
					window.frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the application.
	 */
	public MTSframe() {
		initialize();
	}

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		frame = new JFrame();
		frame.setDefaultCloseOperation(JFrame.DO_NOTHING_ON_CLOSE);
		frame.setBounds(100, 100, 450, 350);
		frame.getContentPane().setLayout(null);
		
		
		
		JButton btnNewButton = new JButton("Conectar");
		btnNewButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				
				GridLayout GL = (GridLayout) IF.getContentPane().getLayout();
				int R = GL.getRows();
				
				R++;
				GL.setRows(R);
				JButton BUTTON = new JButton("MyButton"+R);
				
				BUTTON.setSize(100, 100);
				BUTTON.setVisible(true);
				
				IF.getContentPane().add(BUTTON);
				IF.invalidate();
				IF.validate();
				IF.repaint();
			}
		});
		btnNewButton.setBounds(10, 11, 96, 23);
		frame.getContentPane().add(btnNewButton);
		
		JButton btnSalir = new JButton("Salir");
		btnSalir.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				frame.dispose();
			}
			
		});
		btnSalir.setBounds(10, 45, 96, 23);
		frame.getContentPane().add(btnSalir);
		
		JInternalFrame internalFrame = new JInternalFrame("New JInternalFrame");
		internalFrame.setBounds(116, 11, 308, 290);
		frame.getContentPane().add(internalFrame);
		
		internalFrame.getContentPane().setLayout(new GridLayout(1, 0, 0, 0));
		internalFrame.setVisible(true);
		IF = internalFrame;
		

	}
}
