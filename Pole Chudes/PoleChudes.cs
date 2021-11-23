using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Pole_Chudes
{
    public partial class PoleChudes : Control
    {
        public PoleChudes() : base() //Конструктор
        {
            mashtab = 1.5f;
            this.DoubleBuffered = true;
            loadQuestions();
            setQuestion();
            Width = (int)(551 * mashtab);
            Height = (int)(417 * mashtab);

            //this.Cursor = Cursors.Hand;

            //FormBorderStyle = FormBorderStyle.FixedSingle;
            countChanger.Parent = this;
            countChanger.Location = new Point(145, 250);
            countChanger.BackColor = Color.DarkBlue;
            countChanger.Minimum = 2;
            countChanger.Maximum = 4;
            countChanger.Scroll += new EventHandler(countChanger_Scroll);
            countChanger.Size = new Size(110, 30);

            rCountChanger.Parent = this;
            rCountChanger.Location = new Point(295, 250);
            rCountChanger.BackColor = Color.DarkBlue;
            rCountChanger.Minimum = 1;
            rCountChanger.Maximum = 5;
            rCountChanger.Scroll += new EventHandler(rCountChanger_Scroll);
            rCountChanger.Size = new Size(110, 30);

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = new TextBox();
                names[i].Parent = this;
                names[i].Text = "Игрок " + (i + 1);
                names[i].Location = new Point(150, 300 + i * 25);
            }
            names[2].Visible = false;
            names[3].Visible = false;

            menu.Parent = this;
            menu.Location = new Point(0, 0);
            menu.Size = new Size(551, 417);
            menu.Paint += new PaintEventHandler(menu_Paint);
            menu.MouseClick += new MouseEventHandler(menu_Clicked);
            //menu.Visible = false;
            //openMenu();

            //Кнопка "крутить"
            pictureBox5.Parent = this;
            pictureBox5.Location = new Point(350, 300);
            //pictureBox5.Size = new Size(Width / 6, 25);
            pictureBox5.Size = new Size(100, 25);
            pictureBox5.Paint += new PaintEventHandler(pictureBox5_Paint);
            pictureBox5.MouseClick += new MouseEventHandler(OnPictureBox5Clicked);

            //Барабан
            pictureBox1.Parent = this;
            pictureBox1.Location = new Point(150 * scale, 65 * scale);
            //pictureBox1.Bounds = new Rectangle(10, 10, this.Width - 20, this.Height - 50);
            pictureBox1.Size = new Size(100 * scale, 100 * scale);
            //pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);

            //Поле барабана
            pictureBox2.Parent = this;
            pictureBox2.Location = new Point(150 * scale + pictureBox1.Width + 20, 65 * scale + pictureBox1.Height / 2 - 9);
            pictureBox2.Size = new Size(30, 20);
            //pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Paint += new PaintEventHandler(pictureBox2_Paint);

            //Данные об игроке
            pictureBox3.Parent = this;
            pictureBox3.Location = new Point(420, 0);
            pictureBox3.Size = new Size(135, 80);
            //pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Paint += new PaintEventHandler(pictureBox3_Paint);

            //Леонид Аркадьевич
            pictureBox4.Parent = this;
            pictureBox4.Location = new Point(0, 140);
            pictureBox4.Size = new Size(250, 192);
            //pictureBox4.BorderStyle = BorderStyle.FixedSingle;
            pictureBox4.Paint += new PaintEventHandler(pictureBox4_Paint);

            timer.Interval = 25;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);

            timer2.Interval = 500;
            timer2.Enabled = false;
            timer2.Tick += new EventHandler(timer_Tick2);
            widthLines = 10;
            widthLines *= scale;
            beepShift = 7;
            beepPower = 250;
            angleCircle = 30;
            letterSize = 25;
            xWord = 75;
            yWord = 100;
            alphabetStepY = 0;
            alphabetStepX = 0;
            shadowStep = 2;
            cnt = 0;
            //answer = false;

            cursorPlayer = 0;
            PlayerCount = 2;
            players = new Player[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new Player(i);
                players[i].Name = "";
            }
            onAnimation = false;
            activeAlpha = false;
            activePlus = false;
            isStatistics = false;
            rCursor = 0;
            rCount = 2;
            backgroundColor = HexToSolidBrush("#00005CFF");
        }

        //Список переменных
        int playerCount;
        public virtual int PlayerCount
        {
            get
            {
                return playerCount;
            }
            set
            {
                if (value > 1 && value < 5 && value != playerCount)
                {
                    playerCount = value;
                    players = new Player[playerCount];
                    for (int i = 0; i < playerCount; i++)
                    {
                        players[i] = new Player(i);
                    }
                }
            }
        }
        //bool answer;
        Player[] players;
        int cursorPlayer;
        float mashtab;
        int beepShift;
        int beepPower;
        Color bufColor;
        int nowQuestion;
        string nowWord;
        int angleCircle;
        int letterSize;
        int xWord;
        int yWord;
        int alphabetStepY;
        int alphabetStepX;
        int shadowStep;
        int cnt;
        PictureBox pictureBox1 = new PictureBox();
        PictureBox pictureBox2 = new PictureBox();
        public PictureBox pictureBox3 = new PictureBox();
        PictureBox pictureBox4 = new PictureBox();
        PictureBox pictureBox5 = new PictureBox();
        PictureBox menu = new PictureBox();
        TrackBar countChanger = new TrackBar();
        TrackBar rCountChanger = new TrackBar();
        TextBox[] names = new TextBox[4];
        int scale = 2;
        int widthLines;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        float angle = 0;
        int c = 0;
        bool direction = true;
        float speed = 0f;
        int animPower;
        public Color needColor;
        bool colorChanged = false;
        string[] sections = new string[32] {
            "150","0", "50", "100", "200", "+", "50", "300", "500", "0",
            "150", "П", "50", "100", "200", "0", "50", "300", "500", "Б",
            "150", "0", "50", "100", "200", "450", "50", "300", "500", "0" ,
            //"+", "+", "+", "+", "+", "+", "+", "+", "+", "+" ,
            "0", "550"
        };
        int cursorSection = 0;
        public bool onAnimation;
        public bool activeAlpha;
        public bool activePlus;
        public bool isStatistics;
        int rCount;
        int rCursor;
        SolidBrush backgroundColor;
        //Функция выбора количества игроков
        private void countChanger_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if (i >= countChanger.Value)
                {
                    names[i].Visible = false;
                }
                else
                {
                    names[i].Visible = true;
                }
            }
        }
        //Функция выбора количества раундов 
        private void rCountChanger_Scroll(object sender, EventArgs e)
        {
            rCount = rCountChanger.Value * 2;
            menu.Invalidate();
        }

        //Функция изменения имени игрока
        public void changeName(string value, int number)
        {
            if (value != players[number].Name && value.Length <= 15)
            {
                players[number].Name = value;
            }
        }
        //Функция создания файла README при его отсутствии
        public void addREADME()
        {
            StreamWriter f = new StreamWriter("README.txt");
            f.WriteLine("!!!ПРИМЕЧАНИЯ ДЛЯ ПОЛЬЗОВАТЕЛЯ!!!");
            f.WriteLine("");
            f.WriteLine("Вопросы для игры берутся из файла questions.txt.");
            f.WriteLine("Если данный файл отсутствует, то следует запустить игру и будет создан данный файл с заранее заготовленными 12 вопросами.");
            f.WriteLine("Чтобы корректно добавить в игру новые вопросы, требуется писать новый вопрос в одну строку.");
            f.WriteLine("Следом за вопросом, на следующей строке - соответствующий вопросу ответ в нижнем регистре.");
            f.WriteLine("В ответе не должно присутствовать ничего, кроме кириллицы (никаких цифр или специальных символов, вроде пробела или знаков припинания).");
            f.WriteLine("Максимальное количество символов для вопроса - 187.");
            f.WriteLine("");
            f.WriteLine("Пример:");
            f.WriteLine("Новый добавленный вопрос");
            f.WriteLine("ответ");
            f.WriteLine("Второй новый добавленный вопрос");
            f.WriteLine("второйответ");
            f.Close();
        }

        //Функция создания файла с вопросами при его отсутствии
        public void addFileQuestions()
        {
            StreamWriter f = new StreamWriter("questions.txt");
            for (int i = 0; i < standartQuestions.Length; i++)
            {
                f.WriteLine(standartQuestions[i]);
                f.WriteLine(standartWords[i]);
            }
            f.Close();
        }
        //Функция прогрузки файла вопросов из директории игры
        public void loadQuestions()
        {
            try
            {
                StreamReader ftest = new StreamReader("README.txt");
            }
            catch
            {
                addREADME();
            }
            try
            {
                StreamReader ftest = new StreamReader("questions.txt");
            }
            catch
            {
                addFileQuestions();
            }

            string[] s = File.ReadAllLines("questions.txt");
            questions = new string[s.Length / 2];
            words = new string[s.Length / 2];
            int j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i % 2 == 0)
                {
                    s[i] = lineBreak(s[i], 42);
                    questions[j] = s[i];
                }
                else
                {
                    words[j] = s[i];
                    j++;
                }
            }
        }
        //Перенос строки
        public string lineBreak(string s, int cursorBreaker)
        {
            string buf = "";
            string buf2 = "";
            int cursorSpace = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    cursorSpace = i;
                }
                //buf += s[i];
                
                if (i % cursorBreaker == 0 && i != 0)
                {
                    for (int j = 0; j < cursorSpace; j++)
                    {
                        buf += s[j];
                    }
                    for (int j = cursorSpace + 1; j <s.Length; j++)
                    {
                        buf2 += s[j];
                    }
                    s = buf + "\n" + buf2;
                    buf = "";
                    buf2 = "";
                }
            }
            return s;
        }

        protected string[] questions;
        protected string[] words;
        protected string[] buf;
        //Стандартные заготовленные вопросы, включающиеся при отсутствии файла вопросов в папке с игрой
        protected string[] standartQuestions =
        {
            "Так в старину называли сторожа городских ворот",
            "Эта птица может летать спиной вперёд",
            "Единственное ядовитое млекопитающее в мире",
            "Геометрический термин, широко используемый в характеристике современной техники",
            "У этих животных, считающихся в некоторых странах деликатесом, зубы расположены на языке",
            "Живопись по сырой штукатурке водяными красками",
            "Этот недуг не позволил Илье Репину в преклонном возрасте исправить картину Иван Грозный и сын его Иван",
            "Русский народный танец с быстрой сменой фигур и разнообразными кружениями",
            "Этот материал был известен в Египте и Месотамии, но в современном виде был получен только в XVII веке",
            "Название этого инструмента произошло от первого слова песни, которая чаще всего на нём исполнялась",
            "Как у западных и южных славян назывались: селение, деревня, курень?",
            "Как называлось устье русской печи?"
        };

        //Ответы
        protected string[] standartWords =
        {
           "вратарь",
           "колибри",
           "утконос",
           "диагональ",
           "улитка",
           "фреска",
           "дальтонизм",
           "метелица",
           "хрусталь",
           "шарманка",
           "жупа",
           "хайло"
        };
        //Метод доступа к курсору (итератору) игроков
        protected virtual int CursorPlayer
        {
            get
            {
                return cursorPlayer;
            }
            set
            {
                if (value > playerCount-1)
                {
                    value = 0;
                }
                if (value != cursorPlayer)
                {
                    cursorPlayer = value;
                }
            }
        }
        //Преобразование HEX-кода в SolidBrush
        public static SolidBrush HexToSolidBrush(string hex)
        {
            return new SolidBrush(
                    Color.FromArgb(
                            Convert.ToByte(hex.Substring(7, 2), 16),
                            Convert.ToByte(hex.Substring(1, 2), 16),
                            Convert.ToByte(hex.Substring(3, 2), 16),
                            Convert.ToByte(hex.Substring(5, 2), 16)
                        )
                );
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //Фон
            e.Graphics.Clear(backgroundColor.Color);
            //e.Graphics.FillRectangle(backgroundColor, 0, 0, Width, Height);
            //Рамка для вопроса
            e.Graphics.FillRectangle(new SolidBrush(Color.Orange), 0, Height - 85, Width - Width / 3 + 5, Height / 4);
            //Фон для вопроса
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), 0, Height - 80, Width - Width / 3, Height / 4);
            //Удаление угла
            e.Graphics.FillRectangle(backgroundColor, Width - Width / 3 + 5 - angleCircle, Height - 85, angleCircle, angleCircle);
            //Рамка для алфавита
            e.Graphics.FillRectangle(new SolidBrush(Color.Orange), Width - (Width / 3) - 10, Height - 70, 200, 25);
            //Фон для алфавита
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), Width - (Width / 3) - 10, Height - 65, Width / 2, Height / 4);
            //Скругление угла
            e.Graphics.FillEllipse(new SolidBrush(Color.Orange), Width - Width / 3 + 5 - angleCircle * 2, Height - 85 - 1, angleCircle * 2, angleCircle * 2);
            e.Graphics.FillEllipse(new SolidBrush(Color.DarkBlue), Width - Width / 3 + 5 - angleCircle * 2 - 5, Height - 85 + 5 - 1, angleCircle * 2, angleCircle * 2);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), Width - Width / 3 + 5 - angleCircle * 2, Height - 85 + 5, angleCircle, angleCircle);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), Width - Width / 3 + 5 - angleCircle * 2 + 25, Height - 85 + angleCircle, angleCircle, angleCircle);
            //Вывод вопроса
            Font questionFont = new Font("Comic Sans MS", 10);
            e.Graphics.DrawString(questions[nowQuestion], questionFont, new SolidBrush(Color.Black), 5 + shadowStep, Height - 75 + shadowStep);
            e.Graphics.DrawString(questions[nowQuestion], questionFont, new SolidBrush(Color.Yellow), 5, Height - 75);
            //Вывод алфавита
            Font alphabetFont = new Font("Comic Sans MS", 10);
            alphabetStepY = 0;
            for (int i = 0; i < letters.Length; i++)
            {
                if (i % 10 == 0)
                {
                    alphabetStepY++;
                    alphabetStepX = 0;
                }
                if (!usedLetters.Contains(letters[i]))
                {
                    e.Graphics.DrawString(letters[i], questionFont, new SolidBrush(Color.Black), (Width - 170) + 16 * alphabetStepX  + shadowStep, Height - 80 + 15 * alphabetStepY + shadowStep);
                    e.Graphics.DrawString(letters[i], questionFont, new SolidBrush(Color.Yellow), (Width - 170) + 16 * alphabetStepX, Height - 80 + 15 * alphabetStepY);
                }
                alphabetStepX++;
            }
            //Буквы
            for (int i = 0; i < words[nowQuestion].Length; i++)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), xWord + i * letterSize, yWord, letterSize, letterSize);
                e.Graphics.DrawRectangle(new Pen(Color.Yellow), xWord + i * letterSize, yWord, letterSize, letterSize);
            }
            //Вывод найденных букв
            cnt = 0;
            for (int i = 0; i < nowWord.Length; i++)
            {
                if (usedLetters.Contains(nowWord[i].ToString().ToUpper()))
                {
                    e.Graphics.DrawString(nowWord[i].ToString().ToUpper(), questionFont, new SolidBrush(Color.Yellow), xWord + i * letterSize + 6, yWord + 2);
                    cnt++;
                    checkWin();
                }
            }
            //Метка выбранной ячейки барабана
            e.Graphics.FillPolygon(new SolidBrush(Color.Red), new Point[] {
                new Point(pictureBox1.Location.X + pictureBox1.Width + 2, pictureBox1.Location.Y + pictureBox1.Height / 2),
                new Point(pictureBox1.Location.X + pictureBox1.Width + 2 + 12, pictureBox1.Location.Y + pictureBox1.Height / 2 - 6),
                new Point(pictureBox1.Location.X + pictureBox1.Width + 2 + 12, pictureBox1.Location.Y + pictureBox1.Height / 2 + 6)
            });
            //Текущий раунд
            Font roundFont = new Font("Comic Sans MS", 14);
            e.Graphics.DrawString("Раунд " + (rCursor + 1), roundFont, new SolidBrush(Color.Black), Width / 3+shadowStep, 30+shadowStep);
            e.Graphics.DrawString("Раунд " + (rCursor + 1), roundFont, new SolidBrush(Color.Yellow), Width/3, 30);

            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue),0,0,25,25);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Orange),5),-5,-5,30,30);
            e.Graphics.DrawString("X", new Font("MV Boli", 12,FontStyle.Bold), new SolidBrush(Color.Yellow), 1, -1);
        }
        //Проверка кликов по областям
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!onAnimation)
            {
                if (activeAlpha)
                {
                    alphabetStepY = 0;
                    for (int i = 0; i < letters.Length; i++)
                    {
                        if (i % 10 == 0)
                        {
                            alphabetStepY++;
                            alphabetStepX = 0;
                        }
                        if ((new Rectangle((Width - 169) + alphabetStepX * 16, Height - 80 + 16 * alphabetStepY + 2, 11, 11).Contains(e.X, e.Y)) && (!usedLetters.Contains(letters[i])))
                        {
                            useLetter(letters[i]);
                            break;
                        }
                        alphabetStepX++;
                    }
                }
                if (activePlus)
                {
                    for (int i = 0; i < words[nowQuestion].Length; i++)
                    {
                        if (new Rectangle(xWord + i * letterSize, yWord, letterSize, letterSize).Contains(e.X, e.Y) && !usedLetters.Contains(nowWord[i].ToString()))
                        {
                            useLetter(nowWord[i].ToString().ToUpper());
                            activePlus = false;
                            break;
                        }
                    }
                }
            }
            if ((new Rectangle(0, 0, 30, 30).Contains(e.X, e.Y)))
            {
                if (MessageBox.Show("Вы действительно хотите выйти в меню?", "Не расстраивайте Леонида Аркадьевича :(", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    openMenu();
                }
            }

        }
        //Отрисовка меню
        void menu_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.Clear(Color.DarkBlue);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Orange),10),0,0,menu.Width,menu.Height);
            Font titleFont = new Font("Comic Sans MS", 25, FontStyle.Bold);
            SolidBrush outline = new SolidBrush(Color.Yellow);
            Point pOutline = new Point(170, 50);
            
            /*Обводка*/{
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X + shadowStep, pOutline.Y + shadowStep);
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X - shadowStep, pOutline.Y - shadowStep);
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X + shadowStep, pOutline.Y - shadowStep);
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X - shadowStep, pOutline.Y + shadowStep);

                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X + shadowStep, pOutline.Y);
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X - shadowStep, pOutline.Y);
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X, pOutline.Y - shadowStep);
                e.Graphics.DrawString("Поле Чудес", titleFont, outline, pOutline.X, pOutline.Y + shadowStep);
            }
            e.Graphics.DrawString("Поле Чудес", titleFont, new SolidBrush(Color.DarkBlue), pOutline.X, pOutline.Y);
            Font startGameFont = new Font("Comic Sans MS", 16);
            if (!isStatistics)
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Orange), 3), 200, menu.Height / 2, 150, 25);
                e.Graphics.DrawString("Начать игру", startGameFont, new SolidBrush(Color.Yellow), 210, menu.Height / 2 - 4);

                e.Graphics.DrawString("Раундов: " + rCount, startGameFont, new SolidBrush(Color.Black), 295 + shadowStep, 293 + shadowStep);
                e.Graphics.DrawString("Раундов: " + rCount, startGameFont, new SolidBrush(Color.Yellow), 295, 293);
            }
            else
            {
                Font statisticsFont = new Font("Comic Sans MS", 16);
                Font statisticsFont2 = new Font("Comic Sans MS", 14);
                e.Graphics.DrawString("Результаты:", statisticsFont, new SolidBrush(Color.Yellow), 210, menu.Height / 4);
                for (int i = 0; i < playerCount; i++)
                    { e.Graphics.DrawString(players[i].Name + ":\t" + players[i].Money, statisticsFont2, new SolidBrush(Color.Yellow), 190, menu.Height / 3 + i * 30); }
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Orange), 3), 190, menu.Height / 2+100, 172, 25);
                e.Graphics.DrawString("В главное меню", startGameFont, new SolidBrush(Color.Yellow), 190, menu.Height / 2 - 4 + 100);
            }
        }
        //Функция проверки кликов в меню
        void menu_Clicked(object sender, MouseEventArgs e)
        {
            if (!isStatistics)
            {
                if ((new Rectangle(200, menu.Height / 2, 150, 25).Contains(e.X, e.Y)))
                {
                    PlayerCount = countChanger.Value;
                    for (int i = 0; i < PlayerCount; i++)
                    {
                        players[i].Name = names[i].Text;
                        players[i].Money = 0;
                        players[i].Points = 0;
                        names[i].Visible = false;
                    }
                    cursorPlayer = 0;
                    rCursor = 0;
                    activeAlpha = false;
                    activePlus = false;
                    onAnimation = false;
                    isStatistics = false;
                    pictureBox5.Visible = true;
                    setQuestion();
                    closeAllMenu();
                }
            }
            else
            {
                if ((new Rectangle(190, menu.Height / 2 + 100, 172, 25).Contains(e.X, e.Y)))
                {
                    openMenu();
                }
            }
            
        }
        //Закрыть все открытые меню
        void closeAllMenu()
        {
            menu.Visible = false;
            countChanger.Visible = false;
            rCountChanger.Visible = false;
            for (int i = 0; i < names.Length; i++) 
                { names[i].Visible = false; }
        }
        //Открыть главное меню
        void openMenu()
        {
            isStatistics = false;
            menu.Visible = true;
            countChanger.Visible = true;
            rCountChanger.Visible = true;
            names[0].Visible = true;
            names[1].Visible = true;
            countChanger.Value = 2;
            rCountChanger.Value = 1;
            rCount = 2;
            playerCount = 2;
            menu.Invalidate();
        }
        //Открыть меню статистики
        void openStatistics()
        {
            isStatistics = true;
            menu.Visible = true;
            countChanger.Visible = false;
            rCountChanger.Visible = false;
            for (int i = 0; i < names.Length; i++)
            { names[i].Visible = false; }
            menu.Invalidate();
        }
        //Прорисовка барабана
        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(backgroundColor, 0, 0, Width, Height);
            e.Graphics.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            e.Graphics.RotateTransform(angle);
            e.Graphics.FillEllipse(new SolidBrush(Color.Red), -51 * scale, -51 * scale, 102 * scale, 102 * scale);
            e.Graphics.FillEllipse(new SolidBrush(Color.White), -50 * scale, -50 * scale, 100 * scale, 100 * scale);
            e.Graphics.FillEllipse(new SolidBrush(Color.Black), -48 * scale, -48 * scale, 96 * scale, 96 * scale);
            e.Graphics.FillEllipse(new SolidBrush(Color.White), -45 * scale, -45 * scale, 90 * scale, 90 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), -36 * scale, -36 * scale, 36 * scale, 36 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), -36 * scale, 36 * scale, 36 * scale, -36 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), 0 * scale, -50 * scale, 0 * scale, 50 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), -50 * scale, 0 * scale, 50 * scale, 0 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), -19 * scale, -47 * scale, 19 * scale, 47 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), -47 * scale, -19 * scale, 47 * scale, 19 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), -47 * scale, 19 * scale, 47 * scale, -19 * scale);
            e.Graphics.DrawLine(new Pen(Color.Black, widthLines), 19 * scale, -47 * scale, -19 * scale, 47 * scale);
            e.Graphics.FillEllipse(new SolidBrush(Color.White), -16 * scale, -16 * scale, 32 * scale, 32 * scale);
            e.Graphics.FillEllipse(new SolidBrush(Color.Gray), -11 * scale, -11 * scale, 22 * scale, 22 * scale);
            e.Graphics.FillEllipse(new SolidBrush(Color.Black), -5 * scale, -5 * scale, 10 * scale, 10 * scale);
        }
        //Значение выбранной ячейки барабана
        void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(backgroundColor, 0, 0, Width, Height);
            Font questionFont = new Font("Comic Sans MS", 10,FontStyle.Bold);
            //Вывод значения поля
            if (colorChanged)
            {
                e.Graphics.DrawString(sections[cursorSection], questionFont, new SolidBrush(Color.Black), shadowStep, shadowStep);
                e.Graphics.DrawString(sections[cursorSection], questionFont, new SolidBrush(Color.White), 0,0);
                colorChanged = false;
            }
        }
        //Статистика текущего игрока
        void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.DarkBlue);
            e.Graphics.DrawLines(new Pen(Color.Orange, 10), new Point[] { new Point(0, 0), new Point(0, pictureBox3.Height), new Point(pictureBox3.Width, pictureBox3.Height) });

            Font pointsFont = new Font("Comic Sans MS", 11);

            e.Graphics.DrawString(players[CursorPlayer].Name, pointsFont, new SolidBrush(Color.Black), 10 + 2, 5 + 2);
            e.Graphics.DrawString(players[CursorPlayer].Name, pointsFont, new SolidBrush(Color.Yellow), 10, 5);

            e.Graphics.DrawString("Деньги:", pointsFont, new SolidBrush(Color.Black), 10+2, 23+5 + 2);
            e.Graphics.DrawString("Деньги:", pointsFont, new SolidBrush(Color.Yellow), 10, 23+5);
            e.Graphics.DrawString(players[CursorPlayer].Money.ToString(), pointsFont, new SolidBrush(Color.Black), pictureBox3.Width / 2 + 4 + 2, 23+5 + 2);
            e.Graphics.DrawString(players[CursorPlayer].Money.ToString(), pointsFont, new SolidBrush(Color.Yellow), pictureBox3.Width / 2 + 4, 23 + 5);


            e.Graphics.DrawString("Очки:", pointsFont, new SolidBrush(Color.Black), 10 + 2, 50 + 2);
            e.Graphics.DrawString("Очки:", pointsFont, new SolidBrush(Color.Yellow), 10, 50);
            e.Graphics.DrawString(players[CursorPlayer].Points.ToString(), pointsFont, new SolidBrush(Color.Black), pictureBox3.Width / 2 + 4 + 2, 50 + 2);
            e.Graphics.DrawString(players[CursorPlayer].Points.ToString(), pointsFont, new SolidBrush(Color.Yellow), pictureBox3.Width / 2 + 4, 50);
        }
        //Прорисовка Леонида Аркадьевича
        void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(backgroundColor, 0, 0, Width, Height);
            //Рубашка
            e.Graphics.FillPolygon(new SolidBrush(Color.Black), new Point[] {
                new Point(90,120),
                new Point(170,130),
                new Point(190,192),
                new Point(-10,192),
                new Point(10,130),
                new Point(90,120),
            });
            e.Graphics.FillPolygon(new SolidBrush(Color.White), new Point[] {
                new Point(90,100),
                new Point(120,115),
                new Point(123,135),
                new Point(120,160),
                new Point(110,pictureBox4.Height),
                new Point(70,pictureBox4.Height),
                new Point(60,160),
                new Point(57,135),
                new Point(60,115),
            });
            
            e.Graphics.DrawLines(new Pen(Color.Gray, 1), new Point[] {
                new Point(90,120),
                new Point(170,130),
                new Point(190,192),
                
            });
            e.Graphics.DrawLines(new Pen(Color.Gray, 1), new Point[] {
                new Point(90,120),
                new Point(10,130),
                new Point(-10,192),
            });

            e.Graphics.FillPolygon(new SolidBrush(Color.Black), new Point[] { 
                new Point(90,150),
                new Point(110,140),
                new Point(110,160),
                new Point(90,150),
                new Point(70,140),
                new Point(70,160),
                //new Point(90,150),
                
                
            });

            //Голова
            e.Graphics.FillPolygon(new SolidBrush(Color.PeachPuff), new Point[] {
                new Point(80,40),
                new Point(100,40),
                new Point(110,43),
                new Point(120,50),
                new Point(130,70),
                new Point(122,120),
                new Point(118,130),
                new Point(100,145),
                new Point(80, 145),
                new Point(63,130),
                new Point(59,120),
                new Point(52,70),
                new Point(63,50),
            });
            //Причёска
            e.Graphics.FillPolygon(new SolidBrush(Color.DarkGray), new Point[] {
                new Point(80,40),
                new Point(100,40),
                new Point(110,43),
                new Point(120,50),
                new Point(130,70),
                new Point(130,85),
                new Point(125,100),
                new Point(124,90),
                new Point(123,80),
                new Point(115,60),
                new Point(110,55),
                new Point(100,60),
                new Point(80,60),
                new Point(70,55),
                new Point(65,60),
                new Point(57,80),
                new Point(57,90),
                new Point(56,100),
                new Point(50,85),
                new Point(50,68),
                new Point(60,50),
                //new Point(),
            });
            //Усы
            int mustachesShiftY = 4;
            int mustachesShiftX = 1;
            e.Graphics.FillPolygon(new SolidBrush(Color.DarkGray), new Point[] {
                new Point(100+mustachesShiftX,102 + mustachesShiftY),
                new Point(105+mustachesShiftX,107 + mustachesShiftY),
                new Point(110+mustachesShiftX,120 + mustachesShiftY),
                new Point(95+mustachesShiftX,115 + mustachesShiftY),
                new Point(90+mustachesShiftX,115 + mustachesShiftY),
                new Point(85+mustachesShiftX,115 + mustachesShiftY),
                new Point(70+mustachesShiftX,120 + mustachesShiftY),
                new Point(73+mustachesShiftX,109 + mustachesShiftY),
                new Point(81+mustachesShiftX,102 + mustachesShiftY),
                new Point(90+mustachesShiftX,105 + mustachesShiftY),
            });
            //Фразы Якубовича
            Font leonid = new Font("Comic Sans MS", 10,FontStyle.Bold);
            Point dialog = new Point(5,0);
            Point dialogShadow = new Point(5+shadowStep, 0+shadowStep);
            if (!activeAlpha && !activePlus)
            {
                e.Graphics.DrawString("-Крутите барабан, " + players[CursorPlayer].Name + "!", leonid, new SolidBrush(Color.Black), dialogShadow);
                e.Graphics.DrawString("-Крутите барабан, " + players[CursorPlayer].Name + "!", leonid, new SolidBrush(Color.White), dialog);
            }
            else if (activeAlpha && !activePlus)
            {
                e.Graphics.DrawString("-Назовите букву, " + players[CursorPlayer].Name + ".", leonid, new SolidBrush(Color.Black), dialogShadow);
                e.Graphics.DrawString("-Назовите букву, " + players[CursorPlayer].Name + ".", leonid, new SolidBrush(Color.White), dialog);
            }
            else 
            {
                e.Graphics.DrawString("-Ну что, " + players[CursorPlayer].Name + ", какую букву \nоткрываем?", leonid, new SolidBrush(Color.Black), dialogShadow);
                e.Graphics.DrawString("-Ну что, " + players[CursorPlayer].Name + ", какую букву \nоткрываем?", leonid, new SolidBrush(Color.White), dialog);
            }
        }
        //Кнопка "Крутить"
        void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Orange), 0, 0, pictureBox5.Width, pictureBox5.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), 3, 3, pictureBox5.Width-6, pictureBox5.Height-6);
            Font buttonFont = new Font("Comic Sans MS", 10, FontStyle.Bold);
            e.Graphics.DrawString("К р у т и т ь", buttonFont, new SolidBrush(Color.Black), 6 + shadowStep, 1 + shadowStep);
            e.Graphics.DrawString("К р у т и т ь",buttonFont,new SolidBrush(Color.Yellow),6,1);
        }
        void OnPictureBox5Clicked(object sender, MouseEventArgs e)
        {
            animation();
            pictureBox5.Visible = false;
        }
        //Функция, выполняющая определённые действия в зависимости от выпавшего поля барабана
        void checkSection()
        {
            if (sections[cursorSection] == "+")
            {
                activePlus = true;
            }
            else if (sections[cursorSection] == "0")
            {
                changePlayer();
            }
            else if (sections[cursorSection] == "П")
            {
                players[CursorPlayer].Money += new Random().Next(3,11)*100;
                activeAlpha = true;
                pictureBox3.Invalidate();
            }
            else if (sections[cursorSection] == "Б")
            {
                players[CursorPlayer].Points = 0;
                changePlayer();
            }
            else
            {
                activeAlpha = true;
            }
        }

        //Функция смены игрока
        public void changePlayer()
        {
            CursorPlayer++;
            if (!activeAlpha && !onAnimation && !activePlus) { pictureBox5.Visible = true; }
            pictureBox3.Invalidate();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (Width != 551 || Height != 417)
            {
                Width = 551;
                Height = 417;
            }
            
            pictureBox4.Invalidate();
            angle += speed;
            pictureBox1.Invalidate();
            needColor = getColor();
            if (bufColor != needColor)
            {
                if (bufColor != null)
                {
                    Console.Beep(beepPower, 10);
                }
                
                
                if (direction)
                {
                    beepPower += beepShift;
                } else if ((beepPower - beepShift) > 35 ) { beepPower -= beepShift; }
                
                bufColor = needColor;
                if (cursorSection < sections.Length - 1)
                {
                    cursorSection++;
                }
                else
                {
                    cursorSection = 0;
                }
                colorChanged = true;
                pictureBox2.Invalidate(); 
            }
        }

        void timer_Tick2(object sender, EventArgs e)
        {
            
            if (direction)
            {
                c++;
                speed++;
                
                if (c == animPower)
                {
                    direction = false;
                }
            } else
            {
                c--;
                speed--;
                if (c == 0)
                {
                    speed = 0f;
                    direction = true;
                    onAnimation = false;
                    checkSection();
                    beepPower = 250;
                    timer2.Enabled = false;
                }
            }

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020;
                return createParams;
            }
        }

        //Функция добавления очков
        public void addPoints()
        {
            if (!onAnimation)
            {
                try
                {
                    players[CursorPlayer].Points += Convert.ToInt32(sections[cursorSection]);
                }
                catch
                {

                }
                pictureBox3.Invalidate();
            }
        }

        //Получение цвета ячейки барабана
        public Color getColor()
        {
            using (Bitmap bmp = new Bitmap(100 * scale, 100 * scale))
            {
                pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                Color nowColor = bmp.GetPixel(100 * scale - 1, 100 * scale / 2);
                return nowColor;
            }
        }
        //Запуск анимации барабана
        public void animation()
        {
            if (!onAnimation && !activeAlpha && !activePlus)
            {
                Random AnimPower = new Random();
                animPower = AnimPower.Next(4, 10);
                bufColor = getColor();
                onAnimation = true;
                timer2.Enabled = true;
            }
        }
        //Функция выбора буквы
        protected void useLetter(string letter)
        {
            if (!onAnimation)
            {
                //answerletter = letter;
                string[] buf = usedLetters;
                usedLetters = new string[usedLetters.Length + 1];
                for (int i = 0; i < buf.Length; i++)
                {
                    usedLetters[i] = buf[i];
                }
                usedLetters[usedLetters.Length - 1] = letter;
                Console.Beep(new Random().Next(900, 1200), 5);
                activeAlpha = false;
                if (nowWord.ToUpper().Contains(letter))
                {
                    addPoints();
                }
                else
                {
                    changePlayer();
                }
                pictureBox5.Visible = true;
                Invalidate();
            }
        }
        //Массив использованных букв
        protected string[] usedLetters = { "" };
        
        //Алфавит
        protected string[] letters =
        {
            "А","Б","В","Г","Д",
            "Е","Ё","Ж","З","И",
            "Й","К","Л","М","Н",
            "О","П","Р","С","Т",
            "У","Ф","Х","Ц","Ч",
            "Ш","Щ","Ъ","Ы","Ь",
            "Э","Ю","Я"
        };
        //Проверка победы
        protected void checkWin()
        {
            if (cnt == nowWord.Length)
            {
                direction = true;
                //animation();
                for (int i = 0; i < PlayerCount; i++)
                {
                    players[i].Money += players[i].Points;
                    players[i].Points = 0;
                }
                rCursor++;
                if (rCursor != rCount)
                {
                    setQuestion();
                }
                else
                {
                    openStatistics();
                }
            }
        }

        //Функция выбора вопроса
        public void setQuestion() 
        {
            Random num = new Random();
            nowQuestion = num.Next(0, questions.Length);
            usedLetters = new string[]{ "" };
            nowWord = words[nowQuestion];
            colorChanged = true;
            Invalidate();
        }
    //Класс игрока
    }
    public class Player
    {
        public Player(int Number)
        {
            money = 0;
            points = 0;
            number = Number;
            name = Name;
        }

        int money;
        int points;
        string name;
        int number;
        //Методы доступа к параметру Деньги
        public virtual int Money
        {
            get
            {
                return money;
            }
            set
            {
                if (value >= 0 || value != money)
                {
                    money = value;
                }
                
            }
        }
        //Методы доступа к параметру Очки
        public virtual int Points
        {
            get
            {
                return points;
            }
            set
            {
                if (value >= 0 || value != points)
                {
                    points = value;
                }
            }
        }
        //Методы доступа к параметру Имя
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == "")
                {
                    name = "Игрок " + (number + 1).ToString();
                }
                else
                {
                    name = value;
                }
            }
        }
        //Методы доступа к параметру Номер
        public virtual int Number
        {
            get
            {
                return number;
            }
        }
    }
}
