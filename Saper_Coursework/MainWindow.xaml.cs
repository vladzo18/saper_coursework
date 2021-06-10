using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Saper_Coursework
{
    public partial class MainWindow : Window
    {
        private GameField gameCellsField = new GameField();

        private DispatcherTimer timer = new DispatcherTimer();
        private int timerTime = 0;

        public int gameHeight { get; set; }
        public int gameWidth { get; set; }
        public int gameMinesAmount { get; set; }
        public string userName { get; set; }

        private string currentComplexity;

        private bool firstClick;

        public MainWindow()
        {
            InitializeComponent();

            gameCellsField.allNotMinedCellsIsOpened += winHeader;
            gameCellsField.minesWasOpened += gameOverHandler;
            gameCellsField.flagAmountChanged += flagesChanged;
            gameCellsField.firstClickWas += firstClickHandler;

            timer.Tick += timerTick;
            timer.Interval = new TimeSpan(0, 0, 1);

            currentComplexity = easyMenuItem.Header.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("save.sap"))
            {

                firstClick = false;
                MessageBoxResult result = MessageBox.Show(
                    "У вас залишилася не завершена гра \n" +
                    "Якщо ви хочете завантажити збережену гру натисніть ДА",
                    this.Title,
                    MessageBoxButton.YesNo
                );

                if (result == MessageBoxResult.Yes)
                {

                    BinaryFormatter serializer = new BinaryFormatter();
                    GameCell[,] gs;

                    using (FileStream fs = new FileStream("save.sap", FileMode.Open))
                    {
                        gs = new GameCell[
                            Int32.Parse(serializer.Deserialize(fs).ToString()),
                            Int32.Parse(serializer.Deserialize(fs).ToString())
                        ];

                        for (int i = 0; i < gs.GetLength(0); i++)
                        {
                            for (int j = 0; j < gs.GetLength(1); j++)
                            {
                                gs[i, j] = new GameCell();

                                gs[i, j].isMine = bool.Parse(serializer.Deserialize(fs).ToString());
                                gs[i, j].isOpened = bool.Parse(serializer.Deserialize(fs).ToString());
                                gs[i, j].isFlaged = bool.Parse(serializer.Deserialize(fs).ToString());
                                gs[i, j].minesAmountAround = Int32.Parse(serializer.Deserialize(fs).ToString());
                                gs[i, j].xCoordinate = Int32.Parse(serializer.Deserialize(fs).ToString());
                                gs[i, j].yCoordinate = Int32.Parse(serializer.Deserialize(fs).ToString());
                            }
                        }

                        gameCellsField.currentMinesAmount = Int32.Parse(serializer.Deserialize(fs).ToString());
                        gameCellsField.currentFlagesAmount = Int32.Parse(serializer.Deserialize(fs).ToString());
                        timerTime = Int32.Parse(serializer.Deserialize(fs).ToString());
                        userName = serializer.Deserialize(fs).ToString();
                        currentComplexity = serializer.Deserialize(fs).ToString();
                    }

                    gameCellsField.CellsField = gs;
                    gameCellsField.buildCells(gameField);

                    labelMinesAmount.Content += gameCellsField.currentMinesAmount.ToString();
                    labelFlagesAmount.Content += gameCellsField.currentFlagesAmount.ToString();
                    labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
                    userNameLabel.Content += userName;


                    timer.Start();

                    easyMenuItem.IsChecked = false;
                    mediumMenuItem.IsChecked = false;
                    hardMenuItem.IsChecked = false;

                    switch (currentComplexity)
                    {
                        case "Новачок":
                            easyMenuItem.IsChecked = true;
                            break;
                        case "Любитель":
                            mediumMenuItem.IsChecked = true;
                            break;
                        case "Майстер":
                            hardMenuItem.IsChecked = true;
                            break;
                    }

                    return;
                }
                else
                {
                    File.Delete("save.sap");
                }
            }
         
                SetUserNameWindow win = new SetUserNameWindow();
                win.Owner = this;
                win.ShowDialog();

                userNameLabel.Content += userName;

                gameCellsField.setFieldSize(10, 10);
                gameCellsField.setMinesAmount(10);

                gameCellsField.drawCells(gameField);
                gameCellsField.placeMines();
                gameCellsField.placeNumbers();

                labelMinesAmount.Content += gameCellsField.currentMinesAmount.ToString();
                labelFlagesAmount.Content += gameCellsField.currentFlagesAmount.ToString();

                timer.Start();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            firstClick = false;

            MessageBoxResult result = MessageBox.Show(
                "Ви впевнені що хочете розпочати нову гру?",
                this.Title,
                MessageBoxButton.YesNoCancel
            );

            if (result == MessageBoxResult.Yes)
            {
                gameCellsField.resetCells();
                gameCellsField.placeMines();
                gameCellsField.placeNumbers();

                timerTime = 0;
                labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
            }
        }
        private void winHeader()
        {
            timer.Stop();

            MessageBox.Show(
                "Ви виграли!!!",
                this.Title
            );

            RatingRow current = new RatingRow();
            current.userName = userName;
            current.Complexity = currentComplexity;
            current.Time = TimeSpan.FromSeconds(timerTime).ToString();

            string str;

            using (FileStream fs = new FileStream("rating.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    str = sr.ReadToEnd();
                }
            }

            using (FileStream fs = new FileStream("rating.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (str != "")
                    {
                        List<RatingRow> list = JsonConvert.DeserializeObject<List<RatingRow>>(str);
                        list.Add(current);
                        sw.Write(JsonConvert.SerializeObject(list));
                    }
                    else
                    {
                        List<RatingRow> list = new List<RatingRow>() { current };
                        sw.WriteLine(JsonConvert.SerializeObject(list));
                    }
                }
            }
        }
        private void gameOverHandler()
        {
            
            timer.Stop();

            MessageBox.Show(
                "Ви проиграли!!!",
                this.Title
            );

            gameCellsField.resetCells();
            gameCellsField.placeMines();
            gameCellsField.placeNumbers();

            timerTime = 0;
            firstClick = false;
            labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();

            timer.Start();
            
        }
        private void timerTick(object sender, EventArgs e)
        {
            if(firstClick)
            {
                timerTime++;
                labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
            }
        }
        private void MenuItem_Pause(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            MessageBox.Show(
               "Пауза",
               this.Title
            );
            timer.Start();
        }
        private void flagesChanged()
        {
            labelFlagesAmount.Content = 
            "Кількіть прапорців: " + gameCellsField.currentFlagesAmount.ToString();
        }
        private void eysyLevelClick(object sender, RoutedEventArgs e)
        {
            if (firstClick)
            {
                firstClick = false;
                MessageBoxResult result = MessageBox.Show(
                    "Ви впевнені? \n" +
                    "Рівень буде перезапущено",
                    this.Title,
                    MessageBoxButton.YesNoCancel
                );

                if (result == MessageBoxResult.No)
                {
                    firstClick = true;
                    (sender as MenuItem).IsChecked = false;
                    return;
                }
            }

            if ((sender as MenuItem).IsChecked)
            {
                mediumMenuItem.IsChecked = false;
                hardMenuItem.IsChecked = false;

                gameCellsField.deleteCells(gameField);
                gameCellsField.setFieldSize(10, 10);
                gameCellsField.setMinesAmount(10);
                gameCellsField.drawCells(gameField);
                gameCellsField.placeMines();
                gameCellsField.placeNumbers();

                timerTime = 0;
                labelMinesAmount.Content =
                "Кількіть мін: " + gameCellsField.currentMinesAmount.ToString();
                labelFlagesAmount.Content =
                "Кількіть прапорців: " + gameCellsField.currentFlagesAmount.ToString();
                currentComplexity = easyMenuItem.Header.ToString();
                firstClick = false;
                labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
            }
        }
        private void mediumLevelClick(object sender, RoutedEventArgs e)
        {
            if (firstClick)
            {
                firstClick = false;
                MessageBoxResult result = MessageBox.Show(
                    "Ви впевнені? \n" +
                    "Рівень буде перезапущено",
                    this.Title,
                    MessageBoxButton.YesNoCancel
                );

                if (result == MessageBoxResult.No)
                {
                    firstClick = true;
                    (sender as MenuItem).IsChecked = false;
                    return;
                }
            }

            if ((sender as MenuItem).IsChecked)
            {
                easyMenuItem.IsChecked = false;
                hardMenuItem.IsChecked = false;

                gameCellsField.deleteCells(gameField);
                gameCellsField.setFieldSize(15, 15);
                gameCellsField.setMinesAmount(25);
                gameCellsField.drawCells(gameField);
                gameCellsField.placeMines();
                gameCellsField.placeNumbers();

                timerTime = 0;
                labelMinesAmount.Content =
                "Кількіть мін: " + gameCellsField.currentMinesAmount.ToString();
                labelFlagesAmount.Content =
                "Кількіть прапорців: " + gameCellsField.currentFlagesAmount.ToString();
                currentComplexity = mediumMenuItem.Header.ToString();
                labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
            }
        }
        private void hardLevelClick(object sender, RoutedEventArgs e)
        {
            if (firstClick)
            {
                firstClick = false;
                MessageBoxResult result = MessageBox.Show(
                    "Ви впевнені? \n" +
                    "Рівень буде перезапущено",
                    this.Title,
                    MessageBoxButton.YesNoCancel
                );

                if (result == MessageBoxResult.No)
                {
                    firstClick = true;
                    (sender as MenuItem).IsChecked = false;
                    return;
                }
            }


            if ((sender as MenuItem).IsChecked)
            {
                easyMenuItem.IsChecked = false;
                mediumMenuItem.IsChecked = false;
                
                gameCellsField.deleteCells(gameField);
                gameCellsField.setFieldSize(30, 30);
                gameCellsField.setMinesAmount(100);
                gameCellsField.drawCells(gameField);
                gameCellsField.placeMines();
                gameCellsField.placeNumbers();

                timerTime = 0;
                labelMinesAmount.Content =
                "Кількіть мін: " + gameCellsField.currentMinesAmount.ToString();
                labelFlagesAmount.Content =
                "Кількіть прапорців: " + gameCellsField.currentFlagesAmount.ToString();
                currentComplexity = hardMenuItem.Header.ToString();
                firstClick = false;
                labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
            }
        }
        private void userLevelClick(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            SetСomplexityWindow win = new SetСomplexityWindow();
            win.Owner = this;

            if (win.ShowDialog().Value)
            {
                if (firstClick)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Ви впевнені? \n" +
                        "Рівень буде перезапущено",
                        this.Title,
                        MessageBoxButton.YesNoCancel
                    );

                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                gameCellsField.deleteCells(gameField);
                gameCellsField.setFieldSize(gameWidth, gameHeight);
                gameCellsField.setMinesAmount(gameMinesAmount);
                gameCellsField.drawCells(gameField);
                gameCellsField.placeMines();
                gameCellsField.placeNumbers();

                timerTime = 0;
                labelMinesAmount.Content =
                "Кількіть мін: " + gameCellsField.currentMinesAmount.ToString();
                labelFlagesAmount.Content =
                "Кількіть прапорців: " + gameCellsField.currentFlagesAmount.ToString();
                currentComplexity = (sender as MenuItem).Header.ToString();
                firstClick = false;
                labelTime.Content = TimeSpan.FromSeconds(timerTime).ToString();
            }
            timer.Start();
        }
        private void openRating(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            RatingWindow win = new RatingWindow();
            win.Owner = this;

            string str;

            using (FileStream fs = new FileStream("rating.txt", FileMode.OpenOrCreate, FileAccess.Read)) {
                using(StreamReader sr = new StreamReader(fs))
                {
                    str = sr.ReadToEnd();
                }
            }

            if(str != "")
            {
                List<RatingRow> list = JsonConvert.DeserializeObject<List<RatingRow>>(str);
                win.raiting = list;
                win.generateTable();
            }
          
            win.ShowDialog();
            win.Close();

            timer.Start();
        }
        private void firstClickHandler()
        {
            firstClick = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (firstClick)
            {
                firstClick = false;
                MessageBoxResult result = MessageBox.Show(
                    "Ви впевнені що хочете вийти? \n" +
                    "Якщо ви хочете зберегти дану гру натисніть ДА",
                    this.Title,
                    MessageBoxButton.YesNo
                );

                if (result == MessageBoxResult.Yes)
                {
                    BinaryFormatter serializer = new BinaryFormatter();

                    if (File.Exists("save.sap"))
                    {
                        File.Delete("save.sap");
                    }

                    using (FileStream fs = new FileStream("save.sap", FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, gameCellsField.fieldHeigth);
                        serializer.Serialize(fs, gameCellsField.fieldWidth);

                        foreach (GameCell cell in gameCellsField.CellsField)
                        {
                            serializer.Serialize(fs, cell.isMine);
                            serializer.Serialize(fs, cell.isOpened);
                            serializer.Serialize(fs, cell.isFlaged);
                            serializer.Serialize(fs, cell.minesAmountAround);
                            serializer.Serialize(fs, cell.xCoordinate);
                            serializer.Serialize(fs, cell.yCoordinate);
                        }

                        serializer.Serialize(fs, gameCellsField.currentMinesAmount);
                        serializer.Serialize(fs, gameCellsField.currentFlagesAmount);
                        serializer.Serialize(fs, timerTime);
                        serializer.Serialize(fs, userName);
                        serializer.Serialize(fs, currentComplexity);
                    }
                }
                else
                {
                    if (File.Exists("save.sap"))
                    {
                        File.Delete("save.sap");
                    }
                }
            }
           
        } 
        private void endGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
