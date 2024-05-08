using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace Calculator;

public class frmMain : Form
{
	private string opr = "";

	private double operand = 0.0;

	private bool flagOpPressed = false;

	private bool flagOpAgain = false;

	private bool flagEnter = false;

	private bool flagDeg = false;

	private bool flagShift = false;

	private bool flagSpeak = false;

	private bool flagAB = true;

	private bool flagStd = true;

	private bool flagAnnouncement = false;

	private bool flagClickSound = false;

	private int digiCnt = 0;

	private SoundPlayer clickSound = new SoundPlayer("Click.wav");

	private SpeechSynthesizer syn = new SpeechSynthesizer();

	private IContainer components = null;

	private Button btn1;

	private Button btn2;

	private Button btn3;

	private Button btn6;

	private Button btn5;

	private Button btn4;

	private Button btn9;

	private Button btn8;

	private Button btn7;

	private Button btnDot;

	private Button btn0;

	private Button btnEqu;

	private Button btnCE;

	private Button btnAdd;

	private Button btnSqrt;

	private Button btnSub;

	private Button btnSqr;

	private Label lblID;

	private Button btnMul;

	private Button btnDiv;

	private Button btnNeg;

	private Button btnC;

	private Button btnBack;

	private Button btnRep;

	private Button btnStd;

	private Button btnLg10;

	private Button btnPow;

	private Button btnExp;

	private Button btnLn;

	private Panel pnlSci;

	private Button btnShift;

	private Button btnCopy;

	private Button btnSpk;

	private Label lblEqu;

	private Label lblSpkA;

	private Button btnMod;

	private Label lblMod;

	private Button btnDeg;

	private Label lblDeg;

	private TextBox txtEqn;

	private TextBox txtBrand;

	private TextBox txtModel;

	private Label lblResults;

	private Panel panel1;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button button4;

	private Button button5;

	private Button button6;

	private Button button7;

	private Button button8;

	private Panel pnlbnk;

	private Button btnDmy08;

	private Button btnDmy04;

	private Button btnDmy07;

	private Button btnDmy03;

	private Button btnDmy06;

	private Button btnDmy02;

	private Button btnDmy05;

	private Button btnDmy01;

	private Button btnAB;

	private Label lblSpkB;

	public frmMain()
	{
		InitializeComponent();
	}

	private void frmMain_Load(object sender, EventArgs e)
	{
		pnlSci.Visible = false;
		pnlbnk.Visible = true;
		lblID.Focus();
	}

	private void lblID_Click(object sender, EventArgs e)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		GuidAttribute attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), inherit: true)[0];
		Clipboard.SetText(attribute.Value.ToString());
		lblID.Focus();
	}

	private void frmMain_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void numPad_Click(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		string num = btn.Text;
		string temp;
		if (flagEnter)
		{
			temp = "";
			flagEnter = false;
		}
		else
		{
			temp = lblResults.Text;
		}
		string text = num;
		string text2 = text;
		if (text2 == ".")
		{
			if (!temp.Contains('.'))
			{
				temp += ".";
				txtEqn.Text += ".";
			}
		}
		else
		{
			if (temp == "0")
			{
				temp = "";
			}
			if (flagOpPressed)
			{
				temp = "";
			}
			temp += num;
			txtEqn.Text += num;
		}
		lblResults.Text = temp;
		if (flagClickSound)
		{
			clickSound.Play();
		}
		if (flagOpPressed)
		{
			temp = "";
			digiCnt = 0;
			flagOpPressed = false;
		}
		if (flagEnter)
		{
			txtEqn.Text = temp;
		}
		if (flagOpAgain)
		{
			flagOpAgain = false;
		}
		if (flagEnter)
		{
			txtEqn.Text += num;
			flagEnter = false;
		}
		lblID.Focus();
	}

	private void operator_Click(object sender, EventArgs e)
	{
		if (!flagOpPressed)
		{
			flagEnter = true;
			btnEqu.PerformClick();
			flagOpPressed = true;
			flagOpAgain = false;
		}
		else
		{
			flagOpAgain = true;
		}
		if (flagClickSound)
		{
			clickSound.Play();
		}
		operand = double.Parse(lblResults.Text);
		if (flagEnter)
		{
			txtEqn.Text = lblResults.Text;
			flagEnter = false;
		}
		if (lblResults.Text == "0")
		{
			txtEqn.Text = "0";
		}
		Button btn = (Button)sender;
		opr = btn.Tag.ToString();
		if (flagOpAgain)
		{
			txtEqn.Text = txtEqn.Text.Substring(0, txtEqn.Text.Length - 1);
		}
		if (flagOpPressed)
		{
			switch (opr)
			{
			case "Add":
				txtEqn.Text += "+";
				break;
			case "Sub":
				txtEqn.Text += "-";
				break;
			case "Mul":
				txtEqn.Text += "×";
				break;
			case "Div":
				txtEqn.Text += "÷";
				break;
			case "Mod":
				txtEqn.Text += "%";
				break;
			}
		}
		if (!flagOpPressed)
		{
			flagOpPressed = true;
		}
		lblID.Focus();
	}

	private void u_operatorClick(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		string u_opr = btn.Tag.ToString();
		double value = double.Parse(lblResults.Text);
		if (flagClickSound)
		{
			clickSound.Play();
		}
		txtEqn.Text = "";
		switch (u_opr)
		{
		case "Abs":
		{
			string results = Math.Abs(value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "Abs (" + value + ")";
			break;
		}
		case "Rnd":
		{
			string results = Math.Round(value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "Rnd (" + value + ")";
			break;
		}
		case "Neg":
		{
			string results = (0.0 - value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "± (" + value + ")";
			break;
		}
		case "Rep":
		{
			txtEqn.Text = "1/" + value;
			string results = (1.0 / value).ToString();
			lblResults.Text = results;
			break;
		}
		case "Sqrt":
		{
			string results = Math.Sqrt(value).ToString("N10");
			lblResults.Text = results;
			txtEqn.Text = "√" + value;
			break;
		}
		case "Sqr":
		{
			string results = Math.Pow(value, 2.0).ToString("N10");
			lblResults.Text = results;
			txtEqn.Text = value + "²";
			break;
		}
		case "Sin":
		{
			txtEqn.Text = "Sin(" + value + ")";
			if (flagDeg)
			{
				value = value / 180.0 * Math.PI;
			}
			string results = Math.Sin(value).ToString();
			lblResults.Text = results;
			break;
		}
		case "ASin":
		{
			txtEqn.Text = "ASin(" + value + ")";
			if (flagDeg)
			{
				value = value / 180.0 * Math.PI;
			}
			string results = Math.Asin(value).ToString();
			lblResults.Text = results;
			break;
		}
		case "Cos":
		{
			txtEqn.Text = "Cos(" + value + ")";
			if (flagDeg)
			{
				value = value / 180.0 * Math.PI;
			}
			string results = Math.Cos(value).ToString();
			lblResults.Text = results;
			break;
		}
		case "ACos":
		{
			txtEqn.Text = "ACos(" + value + ")";
			if (flagDeg)
			{
				value = value / 180.0 * Math.PI;
			}
			string results = Math.Acos(value).ToString();
			lblResults.Text = results;
			break;
		}
		case "Tan":
		{
			txtEqn.Text = "Tan(" + value + ")";
			if (flagDeg)
			{
				value = value / 180.0 * Math.PI;
			}
			string results = Math.Tan(value).ToString();
			lblResults.Text = results;
			break;
		}
		case "ATan":
		{
			txtEqn.Text = "ATan(" + value + ")";
			if (flagDeg)
			{
				value = value / 180.0 * Math.PI;
			}
			string results = Math.Atan(value).ToString();
			lblResults.Text = results;
			break;
		}
		case "Log":
		{
			string results = Math.Log10(value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "log₁₀(" + value + ")";
			break;
		}
		case "Ln":
		{
			string results = Math.Log(value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "Ln(" + value + ")";
			break;
		}
		case "10x":
		{
			string results = Math.Pow(10.0, value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "10\u02df(" + value + ")";
			break;
		}
		case "ex":
		{
			string results = Math.Pow(Math.E, value).ToString();
			lblResults.Text = results;
			txtEqn.Text = "e\u02df(" + value + ")";
			break;
		}
		}
		Console.WriteLine("result = " + lblResults.Text);
		if (lblResults.Text.Contains("."))
		{
			lblResults.Text = lblResults.Text.TrimEnd('0');
			lblResults.Text = lblResults.Text.TrimEnd('.');
		}
		if (flagAnnouncement)
		{
			syn.Speak("The computational result is " + lblResults.Text);
		}
		lblID.Focus();
	}

	private void btnEqu_Click(object sender, EventArgs e)
	{
		Console.WriteLine("In btnEqu()");
		if (!flagEnter)
		{
			flagEnter = true;
		}
		else
		{
			flagEnter = false;
		}
		double operand2 = double.Parse(lblResults.Text);
		switch (opr)
		{
		case "Add":
			operand += operand2;
			lblResults.Text = operand.ToString();
			break;
		case "Sub":
			operand -= operand2;
			lblResults.Text = operand.ToString();
			break;
		case "Mul":
			operand *= operand2;
			lblResults.Text = operand.ToString();
			break;
		case "Div":
			operand /= operand2;
			lblResults.Text = operand.ToString();
			break;
		case "Mod":
			operand %= operand2;
			lblResults.Text = operand.ToString();
			break;
		default:
			digiCnt = 0;
			break;
		}
		if (flagEnter)
		{
			txtEqn.Text = lblResults.Text;
		}
		opr = "";
		if (flagAnnouncement && flagEnter)
		{
			syn.Speak("Computed to be " + lblResults.Text);
		}
		lblID.Focus();
	}

	private void btnC_Click(object sender, EventArgs e)
	{
		if (flagClickSound)
		{
			clickSound.Play();
		}
		opr = "";
		operand = 0.0;
		flagOpPressed = false;
		flagEnter = false;
		lblResults.Text = "0";
		txtEqn.Text = "";
		operand = 0.0;
		digiCnt = 0;
		txtEqn.Text = "";
		lblID.Focus();
	}

	private void btnCE_Click(object sender, EventArgs e)
	{
		Console.WriteLine("In operator()");
		if (flagClickSound)
		{
			clickSound.Play();
		}
		lblResults.Text = "0";
		Console.WriteLine("digiCnt=" + digiCnt);
		txtEqn.Text = txtEqn.Text.Substring(0, txtEqn.Text.Length - digiCnt);
		lblID.Focus();
	}

	private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
	{
		switch (e.KeyChar.ToString())
		{
		case "0":
			btn0.PerformClick();
			break;
		case "1":
			btn1.PerformClick();
			break;
		case "2":
			btn2.PerformClick();
			break;
		case "3":
			btn3.PerformClick();
			break;
		case "4":
			btn4.PerformClick();
			break;
		case "5":
			btn5.PerformClick();
			break;
		case "6":
			btn6.PerformClick();
			break;
		case "7":
			btn7.PerformClick();
			break;
		case "8":
			btn8.PerformClick();
			break;
		case "9":
			btn9.PerformClick();
			break;
		case "+":
			btnAdd.PerformClick();
			break;
		case "-":
			btnSub.PerformClick();
			break;
		case "*":
			btnMul.PerformClick();
			break;
		case "/":
			btnDiv.PerformClick();
			break;
		case "=":
			btnEqu.PerformClick();
			break;
		case "\r":
			btnEqu.PerformClick();
			break;
		}
	}

	private void btnBack_Click(object sender, EventArgs e)
	{
		if (flagClickSound)
		{
			clickSound.Play();
		}
		Console.WriteLine("txtResult.Len=" + lblResults.Text.Length);
		if (lblResults.Text.Length == 0)
		{
			lblResults.Text = "0";
		}
		if (txtEqn.Text.Length > 0)
		{
			txtEqn.Text = txtEqn.Text.Substring(0, txtEqn.Text.Length - 1);
		}
		if (txtEqn.Text.Length == 0)
		{
			txtEqn.Text = "";
			lblResults.Text = "0";
		}
		lblID.Focus();
	}

	private void btnStd_Click(object sender, EventArgs e)
	{
		if (flagClickSound)
		{
			clickSound.Play();
		}
		Console.WriteLine("flagStd = " + Convert.ToString(flagStd));
		flagStd = !flagStd;
		Console.WriteLine("flagStd = " + Convert.ToString(flagStd));
		if (flagStd)
		{
			btnStd.Text = "SCI";
			lblMod.Text = "Standard";
			pnlSci.Visible = false;
			pnlbnk.Visible = true;
			Console.WriteLine("pnlSci.Visible = " + Convert.ToString(pnlSci.Visible));
		}
		else
		{
			btnStd.Text = "STD";
			lblMod.Text = "Scientific";
			pnlSci.Visible = true;
			pnlbnk.Visible = false;
			Console.WriteLine("pnlSci.Visible = " + Convert.ToString(pnlSci.Visible));
		}
		Console.WriteLine('\r');
		lblID.Focus();
	}

	private void btnShift_Click(object sender, EventArgs e)
	{
		Console.WriteLine("flagShift = " + Convert.ToString(flagShift));
		if (!flagStd)
		{
			Console.WriteLine("flagStd = " + Convert.ToString(flagStd));
			Console.WriteLine("flagShift = " + Convert.ToString(flagShift));
			flagShift = !flagShift;
			if (flagShift)
			{
				btnLg10.Text = "sin";
				btnLg10.Tag = "Sin";
				btnPow.Text = "cos";
				btnPow.Tag = "Cos";
				btnLn.Text = "tan";
				btnLn.Tag = "Tan";
				btnNeg.Text = "Asin";
				btnNeg.Tag = "ASin";
				btnRep.Text = "Acos";
				btnRep.Tag = "ACos";
				btnSqr.Text = "Atan";
				btnSqr.Tag = "ATan";
				btnExp.Text = "Abs";
				btnExp.Tag = "Abs";
				btnSqrt.Text = "Rnd";
				btnSqrt.Tag = "Rnd";
				Console.WriteLine("sin; cos; tan ");
			}
			else
			{
				btnLg10.Text = "log₁₀";
				btnLg10.Tag = "Log";
				btnPow.Text = "10\u02df";
				btnPow.Tag = "10x";
				btnLn.Text = "ln";
				btnLn.Tag = "Ln";
				btnNeg.Text = "±";
				btnNeg.Tag = "Neg";
				btnRep.Text = "1/x";
				btnRep.Tag = "Rep";
				btnSqr.Text = "x²";
				btnExp.Text = "e\u02df";
				btnExp.Tag = "ex";
				btnSqrt.Text = "√";
				btnSqrt.Tag = "Sqrt";
				Console.WriteLine("log; 10\u02df; ln ");
			}
		}
		Console.WriteLine('\r');
		lblID.Focus();
	}

	private void btnDegRad_Click(object sender, EventArgs e)
	{
		if (flagClickSound)
		{
			clickSound.Play();
		}
		if (flagDeg)
		{
			btnDeg.Text = "DEG";
			lblDeg.Text = "RAD";
			flagDeg = false;
		}
		else
		{
			btnDeg.Text = "RAD";
			lblDeg.Text = "DEG";
			flagDeg = true;
		}
		Console.WriteLine('\r');
		lblID.Focus();
	}

	private void btnCopy_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(lblResults.Text);
		lblID.Focus();
	}

	private void btnSpk_Click(object sender, EventArgs e)
	{
		flagSpeak = !flagSpeak;
		if (flagAB)
		{
			if (flagSpeak)
			{
				flagAnnouncement = true;
				btnSpk.Text = "SPK";
				btnSpk.BackColor = Color.DarkGreen;
				btnSpk.ForeColor = Color.White;
				lblSpkA.Text = "Spk A ON";
			}
			else
			{
				flagAnnouncement = false;
				btnSpk.Text = "SPK";
				btnSpk.BackColor = Color.Lime;
				btnSpk.ForeColor = Color.Black;
				lblSpkA.Text = "Spk A OFF";
			}
		}
		else if (flagSpeak)
		{
			flagClickSound = true;
			btnSpk.Text = "SPK";
			btnSpk.BackColor = Color.DarkGreen;
			btnSpk.ForeColor = Color.White;
			lblSpkB.Text = "Spk B ON";
		}
		else
		{
			flagClickSound = false;
			btnSpk.Text = "SPK";
			btnSpk.BackColor = Color.Lime;
			btnSpk.ForeColor = Color.Black;
			lblSpkB.Text = "Spk B OFF";
		}
		Console.WriteLine("Spk " + flagSpeak);
		lblID.Focus();
	}

	private void btnAB_Click(object sender, EventArgs e)
	{
		flagAB = !flagAB;
		if (flagAB)
		{
			btnAB.Text = "A";
			btnAB.BackColor = Color.White;
			btnAB.ForeColor = Color.Black;
		}
		else
		{
			btnAB.Text = "B";
			btnAB.BackColor = Color.Black;
			btnAB.ForeColor = Color.White;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnEqu = new System.Windows.Forms.Button();
            this.btnCE = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSqrt = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.btnSqr = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.btnMul = new System.Windows.Forms.Button();
            this.btnDiv = new System.Windows.Forms.Button();
            this.btnNeg = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRep = new System.Windows.Forms.Button();
            this.btnStd = new System.Windows.Forms.Button();
            this.btnLg10 = new System.Windows.Forms.Button();
            this.btnPow = new System.Windows.Forms.Button();
            this.btnExp = new System.Windows.Forms.Button();
            this.btnLn = new System.Windows.Forms.Button();
            this.pnlSci = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.pnlbnk = new System.Windows.Forms.Panel();
            this.btnDmy08 = new System.Windows.Forms.Button();
            this.btnDmy04 = new System.Windows.Forms.Button();
            this.btnDmy07 = new System.Windows.Forms.Button();
            this.btnDmy03 = new System.Windows.Forms.Button();
            this.btnDmy06 = new System.Windows.Forms.Button();
            this.btnDmy02 = new System.Windows.Forms.Button();
            this.btnDmy05 = new System.Windows.Forms.Button();
            this.btnDmy01 = new System.Windows.Forms.Button();
            this.btnShift = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSpk = new System.Windows.Forms.Button();
            this.lblEqu = new System.Windows.Forms.Label();
            this.lblSpkA = new System.Windows.Forms.Label();
            this.btnMod = new System.Windows.Forms.Button();
            this.lblMod = new System.Windows.Forms.Label();
            this.btnDeg = new System.Windows.Forms.Button();
            this.lblDeg = new System.Windows.Forms.Label();
            this.txtEqn = new System.Windows.Forms.TextBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnAB = new System.Windows.Forms.Button();
            this.lblSpkB = new System.Windows.Forms.Label();
            this.pnlSci.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlbnk.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(19, 470);
            this.btn1.Margin = new System.Windows.Forms.Padding(4);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(71, 60);
            this.btn1.TabIndex = 1;
            this.btn1.TabStop = false;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(97, 470);
            this.btn2.Margin = new System.Windows.Forms.Padding(4);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(71, 60);
            this.btn2.TabIndex = 2;
            this.btn2.TabStop = false;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(176, 470);
            this.btn3.Margin = new System.Windows.Forms.Padding(4);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(71, 60);
            this.btn3.TabIndex = 3;
            this.btn3.TabStop = false;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn6
            // 
            this.btn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(176, 402);
            this.btn6.Margin = new System.Windows.Forms.Padding(4);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(71, 60);
            this.btn6.TabIndex = 6;
            this.btn6.TabStop = false;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn5
            // 
            this.btn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(97, 402);
            this.btn5.Margin = new System.Windows.Forms.Padding(4);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(71, 60);
            this.btn5.TabIndex = 5;
            this.btn5.TabStop = false;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn4
            // 
            this.btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(19, 402);
            this.btn4.Margin = new System.Windows.Forms.Padding(4);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(71, 60);
            this.btn4.TabIndex = 4;
            this.btn4.TabStop = false;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn9
            // 
            this.btn9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(176, 335);
            this.btn9.Margin = new System.Windows.Forms.Padding(4);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(71, 60);
            this.btn9.TabIndex = 9;
            this.btn9.TabStop = false;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn8
            // 
            this.btn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(97, 335);
            this.btn8.Margin = new System.Windows.Forms.Padding(4);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(71, 60);
            this.btn8.TabIndex = 8;
            this.btn8.TabStop = false;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn7
            // 
            this.btn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(19, 335);
            this.btn7.Margin = new System.Windows.Forms.Padding(4);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(71, 60);
            this.btn7.TabIndex = 7;
            this.btn7.TabStop = false;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btnDot
            // 
            this.btnDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDot.Location = new System.Drawing.Point(176, 538);
            this.btnDot.Margin = new System.Windows.Forms.Padding(4);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(71, 60);
            this.btnDot.TabIndex = 10;
            this.btnDot.TabStop = false;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = true;
            this.btnDot.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btn0
            // 
            this.btn0.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.Location = new System.Drawing.Point(19, 538);
            this.btn0.Margin = new System.Windows.Forms.Padding(4);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(149, 60);
            this.btn0.TabIndex = 0;
            this.btn0.TabStop = false;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.numPad_Click);
            // 
            // btnEqu
            // 
            this.btnEqu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEqu.Location = new System.Drawing.Point(255, 538);
            this.btnEqu.Margin = new System.Windows.Forms.Padding(4);
            this.btnEqu.Name = "btnEqu";
            this.btnEqu.Size = new System.Drawing.Size(149, 60);
            this.btnEqu.TabIndex = 15;
            this.btnEqu.TabStop = false;
            this.btnEqu.Tag = "Equ";
            this.btnEqu.Text = "=";
            this.btnEqu.UseVisualStyleBackColor = true;
            this.btnEqu.Click += new System.EventHandler(this.btnEqu_Click);
            // 
            // btnCE
            // 
            this.btnCE.BackColor = System.Drawing.Color.Orange;
            this.btnCE.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCE.Location = new System.Drawing.Point(333, 335);
            this.btnCE.Margin = new System.Windows.Forms.Padding(4);
            this.btnCE.Name = "btnCE";
            this.btnCE.Size = new System.Drawing.Size(71, 60);
            this.btnCE.TabIndex = 14;
            this.btnCE.TabStop = false;
            this.btnCE.Tag = "CE";
            this.btnCE.Text = "CE";
            this.btnCE.UseVisualStyleBackColor = false;
            this.btnCE.Click += new System.EventHandler(this.btnCE_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(255, 402);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 60);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.TabStop = false;
            this.btnAdd.Tag = "Add";
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.operator_Click);
            // 
            // btnSqrt
            // 
            this.btnSqrt.BackColor = System.Drawing.Color.White;
            this.btnSqrt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSqrt.Location = new System.Drawing.Point(237, 68);
            this.btnSqrt.Margin = new System.Windows.Forms.Padding(4);
            this.btnSqrt.Name = "btnSqrt";
            this.btnSqrt.Size = new System.Drawing.Size(71, 60);
            this.btnSqrt.TabIndex = 12;
            this.btnSqrt.TabStop = false;
            this.btnSqrt.Tag = "Sqrt";
            this.btnSqrt.Text = "√";
            this.btnSqrt.UseVisualStyleBackColor = false;
            this.btnSqrt.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // btnSub
            // 
            this.btnSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSub.Location = new System.Drawing.Point(255, 470);
            this.btnSub.Margin = new System.Windows.Forms.Padding(4);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(71, 60);
            this.btnSub.TabIndex = 16;
            this.btnSub.TabStop = false;
            this.btnSub.Tag = "Sub";
            this.btnSub.Text = "-";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.operator_Click);
            // 
            // btnSqr
            // 
            this.btnSqr.BackColor = System.Drawing.Color.White;
            this.btnSqr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSqr.Location = new System.Drawing.Point(159, 68);
            this.btnSqr.Margin = new System.Windows.Forms.Padding(4);
            this.btnSqr.Name = "btnSqr";
            this.btnSqr.Size = new System.Drawing.Size(71, 60);
            this.btnSqr.TabIndex = 17;
            this.btnSqr.TabStop = false;
            this.btnSqr.Tag = "Sqr";
            this.btnSqr.Text = "x²";
            this.btnSqr.UseVisualStyleBackColor = false;
            this.btnSqr.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(16, 174);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(289, 17);
            this.lblID.TabIndex = 18;
            this.lblID.Text = "Done by: Wong CC (161234X) Group A0";
            this.lblID.Click += new System.EventHandler(this.lblID_Click);
            // 
            // btnMul
            // 
            this.btnMul.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMul.Location = new System.Drawing.Point(333, 402);
            this.btnMul.Margin = new System.Windows.Forms.Padding(4);
            this.btnMul.Name = "btnMul";
            this.btnMul.Size = new System.Drawing.Size(71, 60);
            this.btnMul.TabIndex = 19;
            this.btnMul.TabStop = false;
            this.btnMul.Tag = "Mul";
            this.btnMul.Text = "×";
            this.btnMul.UseVisualStyleBackColor = true;
            this.btnMul.Click += new System.EventHandler(this.operator_Click);
            // 
            // btnDiv
            // 
            this.btnDiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiv.Location = new System.Drawing.Point(333, 470);
            this.btnDiv.Margin = new System.Windows.Forms.Padding(4);
            this.btnDiv.Name = "btnDiv";
            this.btnDiv.Size = new System.Drawing.Size(71, 60);
            this.btnDiv.TabIndex = 20;
            this.btnDiv.TabStop = false;
            this.btnDiv.Tag = "Div";
            this.btnDiv.Text = "÷";
            this.btnDiv.UseVisualStyleBackColor = true;
            this.btnDiv.Click += new System.EventHandler(this.operator_Click);
            // 
            // btnNeg
            // 
            this.btnNeg.BackColor = System.Drawing.Color.White;
            this.btnNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNeg.Location = new System.Drawing.Point(0, 68);
            this.btnNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnNeg.Name = "btnNeg";
            this.btnNeg.Size = new System.Drawing.Size(71, 60);
            this.btnNeg.TabIndex = 21;
            this.btnNeg.TabStop = false;
            this.btnNeg.Tag = "Neg";
            this.btnNeg.Text = "±";
            this.btnNeg.UseVisualStyleBackColor = false;
            this.btnNeg.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // btnC
            // 
            this.btnC.BackColor = System.Drawing.Color.Moccasin;
            this.btnC.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.Location = new System.Drawing.Point(333, 267);
            this.btnC.Margin = new System.Windows.Forms.Padding(4);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(71, 60);
            this.btnC.TabIndex = 22;
            this.btnC.TabStop = false;
            this.btnC.Tag = "C";
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = false;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Khaki;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(335, 199);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(71, 60);
            this.btnBack.TabIndex = 23;
            this.btnBack.TabStop = false;
            this.btnBack.Tag = "Back";
            this.btnBack.Text = "DEL";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRep
            // 
            this.btnRep.BackColor = System.Drawing.Color.White;
            this.btnRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRep.Location = new System.Drawing.Point(79, 68);
            this.btnRep.Margin = new System.Windows.Forms.Padding(4);
            this.btnRep.Name = "btnRep";
            this.btnRep.Size = new System.Drawing.Size(71, 60);
            this.btnRep.TabIndex = 24;
            this.btnRep.TabStop = false;
            this.btnRep.Tag = "Rep";
            this.btnRep.Text = "1/x";
            this.btnRep.UseVisualStyleBackColor = false;
            this.btnRep.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // btnStd
            // 
            this.btnStd.BackColor = System.Drawing.Color.IndianRed;
            this.btnStd.ForeColor = System.Drawing.Color.White;
            this.btnStd.Location = new System.Drawing.Point(20, 135);
            this.btnStd.Margin = new System.Windows.Forms.Padding(4);
            this.btnStd.Name = "btnStd";
            this.btnStd.Size = new System.Drawing.Size(69, 31);
            this.btnStd.TabIndex = 25;
            this.btnStd.TabStop = false;
            this.btnStd.Text = "SCI";
            this.btnStd.UseVisualStyleBackColor = false;
            this.btnStd.Click += new System.EventHandler(this.btnStd_Click);
            // 
            // btnLg10
            // 
            this.btnLg10.BackColor = System.Drawing.Color.White;
            this.btnLg10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLg10.Location = new System.Drawing.Point(0, 0);
            this.btnLg10.Margin = new System.Windows.Forms.Padding(4);
            this.btnLg10.Name = "btnLg10";
            this.btnLg10.Size = new System.Drawing.Size(71, 60);
            this.btnLg10.TabIndex = 26;
            this.btnLg10.Tag = "Log";
            this.btnLg10.Text = "log₁₀";
            this.btnLg10.UseVisualStyleBackColor = false;
            this.btnLg10.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // btnPow
            // 
            this.btnPow.BackColor = System.Drawing.Color.White;
            this.btnPow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPow.Location = new System.Drawing.Point(80, 0);
            this.btnPow.Margin = new System.Windows.Forms.Padding(4);
            this.btnPow.Name = "btnPow";
            this.btnPow.Size = new System.Drawing.Size(71, 60);
            this.btnPow.TabIndex = 27;
            this.btnPow.Tag = "10x";
            this.btnPow.Text = "10˟";
            this.btnPow.UseVisualStyleBackColor = false;
            this.btnPow.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // btnExp
            // 
            this.btnExp.BackColor = System.Drawing.Color.White;
            this.btnExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExp.Location = new System.Drawing.Point(237, -1);
            this.btnExp.Margin = new System.Windows.Forms.Padding(4);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(71, 60);
            this.btnExp.TabIndex = 29;
            this.btnExp.Tag = "ex";
            this.btnExp.Text = "e˟";
            this.btnExp.UseVisualStyleBackColor = false;
            this.btnExp.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // btnLn
            // 
            this.btnLn.BackColor = System.Drawing.Color.White;
            this.btnLn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLn.Location = new System.Drawing.Point(159, 0);
            this.btnLn.Margin = new System.Windows.Forms.Padding(4);
            this.btnLn.Name = "btnLn";
            this.btnLn.Size = new System.Drawing.Size(71, 60);
            this.btnLn.TabIndex = 28;
            this.btnLn.Tag = "Ln";
            this.btnLn.Text = "ln";
            this.btnLn.UseVisualStyleBackColor = false;
            this.btnLn.Click += new System.EventHandler(this.u_operatorClick);
            // 
            // pnlSci
            // 
            this.pnlSci.BackColor = System.Drawing.Color.Transparent;
            this.pnlSci.Controls.Add(this.panel1);
            this.pnlSci.Controls.Add(this.btnLg10);
            this.pnlSci.Controls.Add(this.btnPow);
            this.pnlSci.Controls.Add(this.btnExp);
            this.pnlSci.Controls.Add(this.btnLn);
            this.pnlSci.Controls.Add(this.btnNeg);
            this.pnlSci.Controls.Add(this.btnRep);
            this.pnlSci.Controls.Add(this.btnSqr);
            this.pnlSci.Controls.Add(this.btnSqrt);
            this.pnlSci.Location = new System.Drawing.Point(19, 199);
            this.pnlSci.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSci.Name = "pnlSci";
            this.pnlSci.Size = new System.Drawing.Size(308, 128);
            this.pnlSci.TabIndex = 30;
            this.pnlSci.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Location = new System.Drawing.Point(451, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 128);
            this.panel1.TabIndex = 31;
            this.panel1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 60);
            this.button1.TabIndex = 26;
            this.button1.Tag = "Log";
            this.button1.Text = "log₁₀";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(79, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 60);
            this.button2.TabIndex = 27;
            this.button2.Tag = "10x";
            this.button2.Text = "10˟";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(237, -1);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 60);
            this.button3.TabIndex = 29;
            this.button3.Tag = "ex";
            this.button3.Text = "e˟";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(159, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 60);
            this.button4.TabIndex = 28;
            this.button4.Tag = "Ln";
            this.button4.Text = "ln";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(0, 68);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(71, 60);
            this.button5.TabIndex = 21;
            this.button5.TabStop = false;
            this.button5.Tag = "Neg";
            this.button5.Text = "±";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(79, 68);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 60);
            this.button6.TabIndex = 24;
            this.button6.TabStop = false;
            this.button6.Tag = "Rep";
            this.button6.Text = "1/x";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(159, 68);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(71, 60);
            this.button7.TabIndex = 17;
            this.button7.TabStop = false;
            this.button7.Tag = "Sqr";
            this.button7.Text = "x²";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(237, 68);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(71, 60);
            this.button8.TabIndex = 12;
            this.button8.TabStop = false;
            this.button8.Tag = "Sqrt";
            this.button8.Text = "√";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // pnlbnk
            // 
            this.pnlbnk.BackColor = System.Drawing.Color.Transparent;
            this.pnlbnk.Controls.Add(this.btnDmy08);
            this.pnlbnk.Controls.Add(this.btnDmy04);
            this.pnlbnk.Controls.Add(this.btnDmy07);
            this.pnlbnk.Controls.Add(this.btnDmy03);
            this.pnlbnk.Controls.Add(this.btnDmy06);
            this.pnlbnk.Controls.Add(this.btnDmy02);
            this.pnlbnk.Controls.Add(this.btnDmy05);
            this.pnlbnk.Controls.Add(this.btnDmy01);
            this.pnlbnk.Location = new System.Drawing.Point(19, 199);
            this.pnlbnk.Margin = new System.Windows.Forms.Padding(4);
            this.pnlbnk.Name = "pnlbnk";
            this.pnlbnk.Size = new System.Drawing.Size(308, 128);
            this.pnlbnk.TabIndex = 46;
            this.pnlbnk.Visible = false;
            // 
            // btnDmy08
            // 
            this.btnDmy08.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy08.Location = new System.Drawing.Point(237, 0);
            this.btnDmy08.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy08.Name = "btnDmy08";
            this.btnDmy08.Size = new System.Drawing.Size(71, 60);
            this.btnDmy08.TabIndex = 54;
            this.btnDmy08.TabStop = false;
            this.btnDmy08.Tag = "";
            this.btnDmy08.UseVisualStyleBackColor = true;
            // 
            // btnDmy04
            // 
            this.btnDmy04.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy04.Location = new System.Drawing.Point(237, 68);
            this.btnDmy04.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy04.Name = "btnDmy04";
            this.btnDmy04.Size = new System.Drawing.Size(71, 60);
            this.btnDmy04.TabIndex = 50;
            this.btnDmy04.TabStop = false;
            this.btnDmy04.Tag = "";
            this.btnDmy04.UseVisualStyleBackColor = true;
            // 
            // btnDmy07
            // 
            this.btnDmy07.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy07.Location = new System.Drawing.Point(159, 0);
            this.btnDmy07.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy07.Name = "btnDmy07";
            this.btnDmy07.Size = new System.Drawing.Size(71, 60);
            this.btnDmy07.TabIndex = 53;
            this.btnDmy07.TabStop = false;
            this.btnDmy07.Tag = "";
            this.btnDmy07.UseVisualStyleBackColor = true;
            // 
            // btnDmy03
            // 
            this.btnDmy03.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy03.Location = new System.Drawing.Point(159, 68);
            this.btnDmy03.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy03.Name = "btnDmy03";
            this.btnDmy03.Size = new System.Drawing.Size(71, 60);
            this.btnDmy03.TabIndex = 49;
            this.btnDmy03.TabStop = false;
            this.btnDmy03.Tag = "";
            this.btnDmy03.UseVisualStyleBackColor = true;
            // 
            // btnDmy06
            // 
            this.btnDmy06.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy06.Location = new System.Drawing.Point(80, 0);
            this.btnDmy06.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy06.Name = "btnDmy06";
            this.btnDmy06.Size = new System.Drawing.Size(71, 60);
            this.btnDmy06.TabIndex = 52;
            this.btnDmy06.TabStop = false;
            this.btnDmy06.Tag = "";
            this.btnDmy06.UseVisualStyleBackColor = true;
            // 
            // btnDmy02
            // 
            this.btnDmy02.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy02.Location = new System.Drawing.Point(76, 68);
            this.btnDmy02.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy02.Name = "btnDmy02";
            this.btnDmy02.Size = new System.Drawing.Size(71, 60);
            this.btnDmy02.TabIndex = 48;
            this.btnDmy02.TabStop = false;
            this.btnDmy02.Tag = "";
            this.btnDmy02.UseVisualStyleBackColor = true;
            // 
            // btnDmy05
            // 
            this.btnDmy05.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy05.Location = new System.Drawing.Point(0, 0);
            this.btnDmy05.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy05.Name = "btnDmy05";
            this.btnDmy05.Size = new System.Drawing.Size(71, 60);
            this.btnDmy05.TabIndex = 51;
            this.btnDmy05.TabStop = false;
            this.btnDmy05.Tag = "";
            this.btnDmy05.UseVisualStyleBackColor = true;
            // 
            // btnDmy01
            // 
            this.btnDmy01.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDmy01.Location = new System.Drawing.Point(0, 68);
            this.btnDmy01.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmy01.Name = "btnDmy01";
            this.btnDmy01.Size = new System.Drawing.Size(71, 60);
            this.btnDmy01.TabIndex = 47;
            this.btnDmy01.TabStop = false;
            this.btnDmy01.Tag = "";
            this.btnDmy01.UseVisualStyleBackColor = true;
            // 
            // btnShift
            // 
            this.btnShift.BackColor = System.Drawing.Color.LightSalmon;
            this.btnShift.ForeColor = System.Drawing.Color.White;
            this.btnShift.Location = new System.Drawing.Point(97, 135);
            this.btnShift.Margin = new System.Windows.Forms.Padding(4);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(69, 31);
            this.btnShift.TabIndex = 31;
            this.btnShift.TabStop = false;
            this.btnShift.Text = "SHIFT";
            this.btnShift.UseVisualStyleBackColor = false;
            this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.DimGray;
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(255, 137);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(69, 31);
            this.btnCopy.TabIndex = 32;
            this.btnCopy.TabStop = false;
            this.btnCopy.Text = "COPY";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSpk
            // 
            this.btnSpk.BackColor = System.Drawing.Color.Lime;
            this.btnSpk.ForeColor = System.Drawing.Color.Black;
            this.btnSpk.Location = new System.Drawing.Point(333, 137);
            this.btnSpk.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpk.Name = "btnSpk";
            this.btnSpk.Size = new System.Drawing.Size(69, 31);
            this.btnSpk.TabIndex = 33;
            this.btnSpk.TabStop = false;
            this.btnSpk.Text = "SPK";
            this.btnSpk.UseVisualStyleBackColor = false;
            this.btnSpk.Click += new System.EventHandler(this.btnSpk_Click);
            // 
            // lblEqu
            // 
            this.lblEqu.AutoSize = true;
            this.lblEqu.BackColor = System.Drawing.Color.YellowGreen;
            this.lblEqu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEqu.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblEqu.Location = new System.Drawing.Point(31, 50);
            this.lblEqu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEqu.MaximumSize = new System.Drawing.Size(400, 21);
            this.lblEqu.Name = "lblEqu";
            this.lblEqu.Size = new System.Drawing.Size(0, 20);
            this.lblEqu.TabIndex = 34;
            // 
            // lblSpkA
            // 
            this.lblSpkA.AutoSize = true;
            this.lblSpkA.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpkA.Location = new System.Drawing.Point(321, 26);
            this.lblSpkA.Name = "lblSpkA";
            this.lblSpkA.Size = new System.Drawing.Size(73, 15);
            this.lblSpkA.TabIndex = 35;
            this.lblSpkA.Text = "Spk A OFF";
            this.lblSpkA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMod
            // 
            this.btnMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMod.Location = new System.Drawing.Point(255, 335);
            this.btnMod.Margin = new System.Windows.Forms.Padding(4);
            this.btnMod.Name = "btnMod";
            this.btnMod.Size = new System.Drawing.Size(71, 60);
            this.btnMod.TabIndex = 36;
            this.btnMod.TabStop = false;
            this.btnMod.Tag = "Mod";
            this.btnMod.Text = "%";
            this.btnMod.UseVisualStyleBackColor = true;
            this.btnMod.Click += new System.EventHandler(this.operator_Click);
            // 
            // lblMod
            // 
            this.lblMod.AutoSize = true;
            this.lblMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMod.Location = new System.Drawing.Point(20, 36);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new System.Drawing.Size(84, 20);
            this.lblMod.TabIndex = 37;
            this.lblMod.Text = "Standard";
            this.lblMod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDeg
            // 
            this.btnDeg.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDeg.ForeColor = System.Drawing.Color.White;
            this.btnDeg.Location = new System.Drawing.Point(177, 135);
            this.btnDeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeg.Name = "btnDeg";
            this.btnDeg.Size = new System.Drawing.Size(69, 31);
            this.btnDeg.TabIndex = 38;
            this.btnDeg.TabStop = false;
            this.btnDeg.Text = "DEG";
            this.btnDeg.UseVisualStyleBackColor = false;
            this.btnDeg.Click += new System.EventHandler(this.btnDegRad_Click);
            // 
            // lblDeg
            // 
            this.lblDeg.AutoSize = true;
            this.lblDeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeg.Location = new System.Drawing.Point(187, 36);
            this.lblDeg.Name = "lblDeg";
            this.lblDeg.Size = new System.Drawing.Size(48, 20);
            this.lblDeg.TabIndex = 39;
            this.lblDeg.Text = "RAD";
            this.lblDeg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEqn
            // 
            this.txtEqn.BackColor = System.Drawing.Color.Lime;
            this.txtEqn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEqn.Enabled = false;
            this.txtEqn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEqn.ForeColor = System.Drawing.Color.Red;
            this.txtEqn.Location = new System.Drawing.Point(19, 62);
            this.txtEqn.Margin = new System.Windows.Forms.Padding(4);
            this.txtEqn.Name = "txtEqn";
            this.txtEqn.ReadOnly = true;
            this.txtEqn.Size = new System.Drawing.Size(385, 27);
            this.txtEqn.TabIndex = 42;
            this.txtEqn.TabStop = false;
            // 
            // txtBrand
            // 
            this.txtBrand.BackColor = System.Drawing.SystemColors.Control;
            this.txtBrand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBrand.Location = new System.Drawing.Point(9, 1);
            this.txtBrand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(113, 23);
            this.txtBrand.TabIndex = 43;
            this.txtBrand.TabStop = false;
            this.txtBrand.Text = "CASIO";
            this.txtBrand.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.SystemColors.Control;
            this.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtModel.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModel.Location = new System.Drawing.Point(113, 4);
            this.txtModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(124, 19);
            this.txtModel.TabIndex = 44;
            this.txtModel.TabStop = false;
            this.txtModel.Text = "  fx-991ES Plus";
            this.txtModel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblResults
            // 
            this.lblResults.BackColor = System.Drawing.Color.Lime;
            this.lblResults.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(19, 87);
            this.lblResults.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResults.Name = "lblResults";
            this.lblResults.Padding = new System.Windows.Forms.Padding(1);
            this.lblResults.Size = new System.Drawing.Size(385, 46);
            this.lblResults.TabIndex = 45;
            this.lblResults.Text = "0";
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAB
            // 
            this.btnAB.BackColor = System.Drawing.Color.White;
            this.btnAB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAB.ForeColor = System.Drawing.Color.Black;
            this.btnAB.Location = new System.Drawing.Point(336, 174);
            this.btnAB.Margin = new System.Windows.Forms.Padding(4);
            this.btnAB.Name = "btnAB";
            this.btnAB.Size = new System.Drawing.Size(69, 21);
            this.btnAB.TabIndex = 46;
            this.btnAB.TabStop = false;
            this.btnAB.Text = "A";
            this.btnAB.UseVisualStyleBackColor = false;
            this.btnAB.Click += new System.EventHandler(this.btnAB_Click);
            // 
            // lblSpkB
            // 
            this.lblSpkB.AutoSize = true;
            this.lblSpkB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpkB.Location = new System.Drawing.Point(321, 42);
            this.lblSpkB.Name = "lblSpkB";
            this.lblSpkB.Size = new System.Drawing.Size(74, 15);
            this.lblSpkB.TabIndex = 47;
            this.lblSpkB.Text = "Spk B OFF";
            this.lblSpkB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(423, 610);
            this.Controls.Add(this.pnlbnk);
            this.Controls.Add(this.lblSpkB);
            this.Controls.Add(this.btnAB);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtEqn);
            this.Controls.Add(this.lblDeg);
            this.Controls.Add(this.btnDeg);
            this.Controls.Add(this.lblMod);
            this.Controls.Add(this.btnMod);
            this.Controls.Add(this.lblSpkA);
            this.Controls.Add(this.lblEqu);
            this.Controls.Add(this.btnSpk);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnShift);
            this.Controls.Add(this.btnStd);
            this.Controls.Add(this.pnlSci);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.btnDiv);
            this.Controls.Add(this.btnMul);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnEqu);
            this.Controls.Add(this.btnCE);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDot);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Calculator";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.pnlSci.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlbnk.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

	}
}
