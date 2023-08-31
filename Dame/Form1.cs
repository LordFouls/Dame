using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dame
{
    public partial class MainWin : Form
    {
        //Knöpfefont für die jeweiligen Situationen
        Font çoban = new Font("Arial", 28, FontStyle.Regular);
        Font kraliçe = new Font("Arial", 28, FontStyle.Bold);
        //Status ob und welcher Knopf ausgewählt ist
        string clicked_button = "";
        //Knöpfe sind nach A1, B1... benannt. Char und Zahl damit man besser unterscheiden kann.
        //char müssen zum besseren arbeiten in int umgewandelt werden
        Dictionary<char, int> translate4index = new Dictionary<char, int>();
        //Andersherum müssen auch wieder Zahlen in Buchstaben übersetzt werden, um das Objekt zu finden.
        Dictionary<int, char> translate4objekt = new Dictionary<int, char>();


        public void New_game()
        {
            int playground_size = 8;

            for (int number = 0; number < playground_size; number++)
            {
                for (int letter = 0; letter < playground_size; letter++)
                {
                    if (number % 2 == 0 && letter % 2 == 0 || number % 2 == 1 && letter % 2 == 1)
                    {
                        this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].BackColor =
                            Color.White;

                        if (letter < 3)
                        {
                            this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Text = "O";
                            this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Font = çoban;
                        }
                        else if (letter > 4)
                        {
                            this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Text = "X";
                            this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Font = çoban;
                        }
                        else
                        {
                            this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Text = "";
                        }
                    }
                    else
                    {
                        this.Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].BackColor =
                            Color.Black;
                    }
                }
            }
        }
        public MainWin()
        {
            InitializeComponent();

            translate4index.Add('A', 0);
            translate4index.Add('0', 0);
            translate4index.Add('B', 1);
            translate4index.Add('1', 1);
            translate4index.Add('C', 2);
            translate4index.Add('2', 2);
            translate4index.Add('D', 3);
            translate4index.Add('3', 3);
            translate4index.Add('E', 4);
            translate4index.Add('4', 4);
            translate4index.Add('F', 5);
            translate4index.Add('5', 5);
            translate4index.Add('G', 6);
            translate4index.Add('6', 6);
            translate4index.Add('H', 7);
            translate4index.Add('7', 7);

            translate4objekt.Add(0, 'A');
            translate4objekt.Add(1, 'B');
            translate4objekt.Add(2, 'C');
            translate4objekt.Add(3, 'D');
            translate4objekt.Add(4, 'E');
            translate4objekt.Add(5, 'F');
            translate4objekt.Add(6, 'G');
            translate4objekt.Add(7, 'H');

            //füllen des Spielfeldes
            New_game();
            
        }
        private void Form1_Load(object sender, EventArgs e) {
        }
        string Playground_Click(string button, string clicked_button)
        {
            if (clicked_button == "")
            {
                //font farbe wird geändert
                this.Controls.Find(button, true)[0].ForeColor = Color.Red;
                return button;
            }
            else if (clicked_button != "")
            {
                //regeln für das neu setzen
                if
                (
                    //verkürtzt: A*(F+(B*C*D*E)*(G*H+I*J)
                    this.Controls.Find(button, true)[0].Text != this.Controls.Find(clicked_button, true)[0].Text
                    /*A*/ //spielstein nicht gleich
                    && //und
                    (
                        this.Controls.Find(clicked_button, true)[0].Font == kraliçe
                        /*F*/ //spielstein ist dame
                        || //oder
                        (
                            translate4index[button[0]] - translate4index[clicked_button[0]] < 2
                            /*B*/ //buchstabe läuf nicht weiter als 1
                            && //und
                            button[1] - clicked_button[1] < 2
                            /*C*/ //nummer läuf nicht weiter als 1
                            && //und
                            translate4index[button[0]] - translate4index[clicked_button[0]] > -2
                            /*D*/ //buchstabe läuf nicht weiter als -1
                            && //und
                            button[1] - clicked_button[1] > -2
                        /*E*/ //nummer läuf nicht weiter als -1
                        )
                        && //und
                        (
                            (
                                this.Controls.Find(clicked_button, true)[0].Text == "O"
                                /*G*/ //spielstein ist O
                                && //und
                                translate4index[clicked_button[0]] < translate4index[button[0]]
                            /*H*/ //läuft in richtung H
                            )
                            || //oder
                            (
                                this.Controls.Find(clicked_button, true)[0].Text == "X"
                                /*I*/ //spielstein ist X
                                && //und
                                translate4index[clicked_button[0]] > translate4index[button[0]]
                            /*J*/ //läuft in richtung A
                            )
                        )
                    )
                )
                {
                    //vorzeichen wechsel je nach richtung in die der Stein versetzt wird
                    int sing_letter, sing_number;
                    if (translate4index[button[0]] - translate4index[clicked_button[0]] > 0)
                    {
                        sing_letter = 1;
                        //MessageBox.Show("sign_letter plus " + (translate4index[button[0]] - translate4index[clicked_button[0]]));
                    }
                    else
                    {
                        sing_letter = -1;
                        //MessageBox.Show("sign_letter minus " + (translate4index[button[0]] - translate4index[clicked_button[0]]));
                    }
                    if (translate4index[button[1]] - translate4index[clicked_button[1]] > 0)
                    {
                        sing_number = 1;
                        //MessageBox.Show("sign_number plus " + (translate4index[button[1]] - translate4index[clicked_button[1]]));
                    }
                    else
                    {
                        sing_number = -1;
                        //MessageBox.Show("sign_number minus " + (translate4index[button[1]] - translate4index[clicked_button[1]]));
                    }
                    //prüfen ob ein stein im weg liegt
                    int number = 0, letter = 0;
                    for (number = translate4index[clicked_button[1]] + 1 * sing_number,
                         letter = translate4index[clicked_button[0]] + 1 * sing_letter;
                                number != translate4index[button[1]] &&
                                letter != translate4index[button[0]];
                                        number = number + 1 * sing_number,
                                        letter = letter + 1 * sing_letter)
                    {
                        //MessageBox.Show("x_axis: " + translate4objekt[letter] + "\ny_axis: " + (number + 1));
                        if (Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Text != "")
                        {
                            MessageBox.Show("Diese Zug ist nicht zulässig.");
                            goto End;
                        }
                    }
                    //felder müssen auf der diagonalen erreichbar sein
                    if (letter != translate4index[button[0]] ||
                       number != translate4index[button[1]])
                    {
                        MessageBox.Show("Diese Zug ist nicht zulässig.");
                        goto End;
                    }
                    //wenn feld leer
                    if (this.Controls.Find(button, true)[0].Text == "")
                    {
                        //neue posi figur
                        this.Controls.Find(button, true)[0].Text = this.Controls.Find(clicked_button, true)[0].Text;
                        this.Controls.Find(button, true)[0].Font = this.Controls.Find(clicked_button, true)[0].Font;
                        this.Controls.Find(clicked_button, true)[0].Text = "";
                        //MessageBox.Show(clicked_button + " to " + button);//Debug
                    }
                    else //wenn Gegner da ist
                    {
                        //schlagen
                        //überspringen des Steines
                        try
                        {
                            if (this.Controls.Find(Convert.ToString(translate4objekt[translate4index[button[0]] + (1 * sing_letter)]) +
                                    //Ich benutze mein dictionary weil convert chars nicht fehlerfrei üersetzt
                                    Convert.ToString(translate4index[button[1]] + (1 * sing_number))
                                , true)[0].Text != "")
                            {
                                MessageBox.Show("Diese Zug ist nicht zulässig.");
                                goto End;
                            }
                            this.Controls.Find(Convert.ToString(translate4objekt[translate4index[button[0]] + (1 * sing_letter)]) +
                                    //Ich benutze mein dictionary weil convert chars nicht fehlerfrei üersetzt
                                    Convert.ToString(translate4index[button[1]] + (1 * sing_number))
                                , true)[0].Text = this.Controls.Find(clicked_button, true)[0].Text;

                            this.Controls.Find(Convert.ToString(translate4objekt[translate4index[button[0]] + (1 * sing_letter)]) +
                                    //Ich benutze mein dictionary weil convert chars nicht fehlerfrei üersetzt
                                    Convert.ToString(translate4index[button[1]] + (1 * sing_number))
                                , true)[0].Font = this.Controls.Find(clicked_button, true)[0].Font;

                            //MessageBox.Show(button);

                            //löschen beider Steine
                            if (Controls.Find(clicked_button, true)[0].Text == "X")
                            {
                                Score_O.Text = Convert.ToString(Convert.ToInt16(Score_O.Text) + 1);
                            }
                            else
                            {
                                Score_X.Text = Convert.ToString(Convert.ToInt16(Score_X.Text) + 1);
                            }
                            this.Controls.Find(clicked_button, true)[0].Text = this.Controls.Find(button, true)[0].Text = "";

                            //nochmal wegen damen erschaffung
                            button = Convert.ToString(translate4objekt[translate4index[button[0]] + (1 * sing_letter)]) +
                                    //Ich benutze mein dictionary weil convert chars nicht fehlerfrei üersetzt
                                    Convert.ToString(translate4index[button[1]] + (1 * sing_number));
                        }
                        catch
                        {
                            MessageBox.Show("Diese Zug ist nicht zulässig.");
                            goto End;
                        }
                    }
                    //erschaffung einer dame
                    if (button[0] == 'A' || button[0] == 'H')
                    {
                        this.Controls.Find(button, true)[0].Font = kraliçe;
                    }
                }
                else
                {
                    //regel verstoß
                    MessageBox.Show("Diese Zug ist nicht zulässig.");
                }
            }
        End:
            //font farbe zurücksetzen
            this.Controls.Find(clicked_button, true)[0].ForeColor = Color.Black;
            return "";
        }
        private void Click(object sender, EventArgs e)
        {
            // der sender ist der geklickte button und muss eine Objekttyp zugewiesen werden
            if (sender is Button b)
            {
                clicked_button = Playground_Click(b.Name, clicked_button);
            }
        }

        private void new_game_Click(object sender, EventArgs e)
        {
            New_game();
            Score_O.Text = "0";
            Score_X.Text = "0";
        }

        private void Safe_game_Click(object sender, EventArgs e)
        {
            StreamWriter file_dame_write = new StreamWriter(@"..\dame.txt");
            int playground_size = 8;
            for (int number = 0; number < playground_size; number++)
            {
                for (int letter = 0; letter < playground_size; letter++)
                {
                    if (number % 2 == 0 && letter % 2 == 0 || number % 2 == 1 && letter % 2 == 1)
                    {
                        file_dame_write.WriteLine(
                            Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Text);
                        file_dame_write.WriteLine(
                            Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Font);
                    }
                }
            }
            file_dame_write.WriteLine(Score_O.Text);
            file_dame_write.WriteLine(Score_X.Text);
            file_dame_write.Close();
        }

        private void Load_game_Click(object sender, EventArgs e)
        {
            StreamReader file_dame_read = new StreamReader(@"..\dame.txt");
            int playground_size = 8;
            for (int number = 0; number < playground_size; number++)
            {
                for (int letter = 0; letter < playground_size; letter++)
                {
                    if (number % 2 == 0 && letter % 2 == 0 || number % 2 == 1 && letter % 2 == 1)
                    {
                        Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Text = 
                            file_dame_read.ReadLine();
                        string font = file_dame_read.ReadLine();
                        if (font == Convert.ToString(çoban)) {
                            
                            Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Font = çoban;
                        }
                        else
                        {
                            Controls.Find(translate4objekt[letter] + Convert.ToString(number), true)[0].Font = kraliçe;
                        }
                    }
                }
            }
            Score_O.Text = file_dame_read.ReadLine();
            Score_X.Text = file_dame_read.ReadLine();
            file_dame_read.Close();
        }
    }
} 

            