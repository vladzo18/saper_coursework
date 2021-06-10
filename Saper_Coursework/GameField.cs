using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Saper_Coursework
{
    class GameField
    {
        public int fieldHeigth { get; set; }
        public int fieldWidth { get; set; }

        private sbyte cellSize = 25;

        private int currentOpendCells = 0;

        public int currentMinesAmount { get; set; }
        public int currentFlagesAmount { get; set; }
        public GameCell[,] CellsField { get; set; }

        private Random rand = new Random();

        private ResourceDictionary btnStatesImg = 
            (ResourceDictionary)Application.Current.Resources["btnStatesImg"];
        private ResourceDictionary btnNumbersImg = 
            (ResourceDictionary)Application.Current.Resources["btnNumbersImg"];

        public delegate void GameFieldHandler();
        public event GameFieldHandler allNotMinedCellsIsOpened;
        public event GameFieldHandler minesWasOpened;
        public event GameFieldHandler flagAmountChanged;

        private bool firstBtnClick;
        private bool mineWasOpened;
        public event GameFieldHandler firstClickWas;

        public GameField()
        {
            
        }

        public void drawCells(Canvas canvas)
        {
            CellsField = new GameCell[fieldHeigth, fieldWidth];
            currentFlagesAmount = currentMinesAmount;

            for (int y = 0, i = 0; y < fieldHeigth * cellSize; y += cellSize, i++)
            {
                for (int x = 0, j = 0; x < fieldWidth * cellSize; x += cellSize, j++)
                {
                    CellsField[i, j] = new GameCell();

                    CellsField[i, j].Background = (ImageBrush)btnStatesImg["btnClosedImg"];
                    CellsField[i, j].BorderThickness = new Thickness(0);

                    CellsField[i, j].Style = (Style)btnStatesImg["btnStyle"];

                    CellsField[i, j].Width = cellSize;
                    CellsField[i, j].Height = cellSize;

                    CellsField[i, j].xCoordinate = j;
                    CellsField[i, j].yCoordinate = i;

                    CellsField[i, j].PreviewMouseUp += cell_MouseUpHandler;

                    Canvas.SetLeft(CellsField[i, j], x);
                    Canvas.SetTop(CellsField[i, j], y);

                    canvas.Children.Add(CellsField[i, j]);
                }
            }

            canvas.Width = getCanvasWidth();
            canvas.Height = getCanvasHeigth();
        }
        public void buildCells(Canvas canvas)
        {
            fieldHeigth = CellsField.GetLength(0);
            fieldWidth = CellsField.GetLength(1);

            for (int y = 0, i = 0; y < fieldHeigth * cellSize; y += cellSize, i++)
            {
                for (int x = 0, j = 0; x < fieldWidth * cellSize; x += cellSize, j++)
                {
                    CellsField[i, j].Width = cellSize;
                    CellsField[i, j].Height = cellSize;

                    if (CellsField[i, j].isOpened)
                    {
                        if (CellsField[i, j].minesAmountAround == 0)
                        {
                            CellsField[i, j].Background = (ImageBrush)btnNumbersImg["btnZeroImg"];
                        }
                        else
                        {
                            CellsField[i, j].Background = (ImageBrush)btnNumbersImg["btnNum" + CellsField[i, j].minesAmountAround];
                        }
                        CellsField[i, j].Style = (Style)btnStatesImg["btnStyleOpened"];
                    }
                    else
                    {
                        if (CellsField[i, j].isFlaged)
                        {
                            CellsField[i, j].Background = (ImageBrush)btnStatesImg["btnflagedImg"];
                        }
                        else
                        {
                            CellsField[i, j].Background = (ImageBrush)btnStatesImg["btnClosedImg"];
                        }
                        CellsField[i, j].Style = (Style)btnStatesImg["btnStyle"];
                    }
                    CellsField[i, j].BorderThickness = new Thickness(0);

                    CellsField[i, j].PreviewMouseUp += cell_MouseUpHandler;

                    Canvas.SetLeft(CellsField[i, j], x);
                    Canvas.SetTop(CellsField[i, j], y);

                    canvas.Children.Add(CellsField[i, j]);
                }
            }

            canvas.Width = getCanvasWidth();
            canvas.Height = getCanvasHeigth();
        }
        public void placeMines()
        {
            int x, y;
            for (int i = 0; i < currentMinesAmount; i++)
            {
                while (true)
                {
                    x = rand.Next(fieldHeigth);
                    y = rand.Next(fieldWidth);

                    if (!CellsField[x, y].isMine)
                    {
                        CellsField[x, y].isMine = true;
                        break;
                    }
                }
            }
        }
        private List<Point> findMines()
        {
            List<Point> mines = new List<Point>();

            for (int i = 0; i < fieldHeigth; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (CellsField[i, j].isMine)
                    {
                        CellsField[i, j].minesAmountAround = -1;

                        Point temp = new Point();
                        temp.X = i;
                        temp.Y = j;

                        mines.Add(temp);
                    }

                }
            }

            return mines;
        }
        public void placeNumbers()
        {
            List<Point> mines = findMines();

            foreach (Point mine in mines)
            {
                for (int x = (int)mine.X - 1; x <= (int)mine.X + 1; x++)
                {
                    for (int y = (int)mine.Y - 1; y <= (int)mine.Y + 1; y++)
                    {
                        if (x >= 0 && y >= 0 && x < fieldHeigth && y < fieldWidth)
                        {
                            CellsField[x, y].minesAmountAround++;
                        }
                    }
                }
            }

        }
        public void showMines()
        {
            List<Point> mines = findMines();

            foreach (Point mine in mines)
            {
                CellsField[(int)mine.X, (int)mine.Y].isOpened = true;
                CellsField[(int)mine.X, (int)mine.Y].Background = (ImageBrush)btnStatesImg["btnBombImg"];
                CellsField[(int)mine.X, (int)mine.Y].Style = (Style)btnStatesImg["btnStyleOpened"];
            }
        }
        private void openCell(GameCell cell)
        {
            if (cell.minesAmountAround == 0)
            {
                cell.Background = (ImageBrush)btnNumbersImg["btnZeroImg"];
            }
            else
            {
                cell.Background = (ImageBrush)btnNumbersImg["btnNum" + cell.minesAmountAround];
            }

            if (cell.isFlaged)
            {
                cell.isFlaged = !cell.isFlaged;
                currentFlagesAmount++;
                flagAmountChanged?.Invoke();
            }

            cell.Style = (Style)btnStatesImg["btnStyleOpened"];
            cell.isOpened = true;
            currentOpendCells++;
        }
        private void openRegion(GameCell clickedCell)
        {
            GameCell currentCell = clickedCell;;
            openCell(currentCell);

            if (currentCell.minesAmountAround == 0)
            {
                for (int y = currentCell.yCoordinate - 1; y <= currentCell.yCoordinate + 1; y++)
                    {
                        for (int x = currentCell.xCoordinate - 1; x <= currentCell.xCoordinate + 1; x++)
                        {
                            if ((y >= 0 && y < fieldHeigth) && (x >= 0 && x < fieldWidth))
                            {
                                if (x == currentCell.xCoordinate && y == currentCell.yCoordinate)
                                {
                                    continue;
                                }
                                if (!CellsField[y, x].isOpened)
                                {
                                    openRegion(CellsField[y, x]);
                                }
                            }
                        }
                }
                 
            }
        }
        public void resetCells()
        {
            for (int i = 0; i < fieldHeigth; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    CellsField[i, j].isMine = false;
                    CellsField[i, j].minesAmountAround = 0; 
                    CellsField[i, j].isOpened = false;
                    CellsField[i, j].isFlaged = false;
                    CellsField[i, j].Background = (ImageBrush)btnStatesImg["btnClosedImg"];
                    CellsField[i, j].Style = (Style)btnStatesImg["btnStyle"];
                }
            }

            currentOpendCells = 0;
            currentFlagesAmount = currentMinesAmount;
            flagAmountChanged?.Invoke();

            firstBtnClick = false;
        }
        public void deleteCells(Canvas canvas)
        {
            for (int i = 0; i < fieldHeigth; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                   CellsField[i, j].PreviewMouseUp -= cell_MouseUpHandler;

                   CellsField[i, j].Background = null;
                   CellsField[i, j].Style = null;

                   CellsField[i, j] = null;
                }
            }

            CellsField = null;

            canvas.Children.Clear();

            currentOpendCells = 0;
            currentFlagesAmount = 20;

            firstBtnClick = false;

            flagAmountChanged?.Invoke();
        }
        public void setFieldSize(int width, int height)
        {
            fieldHeigth = height;
            fieldWidth = width;
        }
        public void setMinesAmount(int minesAmount)
        {
            currentMinesAmount = minesAmount;
        }
        private void openCellWithoutMine(GameCell gameCell)
        {
            if (gameCell.minesAmountAround == 0)
            {
                openRegion(gameCell);
            }
            else if (gameCell.minesAmountAround > 0)
            {
                gameCell.Background = (ImageBrush)btnNumbersImg["btnNum" + gameCell.minesAmountAround];
                gameCell.isOpened = true;
                gameCell.Style = (Style)btnStatesImg["btnStyleOpened"];

                currentOpendCells++;
            }
        }
        private double getCanvasWidth()
        {
            return CellsField[0, 0].Width * fieldWidth;
        }
        private double getCanvasHeigth()
        {
            return CellsField[0, 0].Height * fieldHeigth;
        }
        private void leftClick(GameCell gameCell)
        {
            if (!gameCell.isOpened && !gameCell.isFlaged)
            {
                if (gameCell.isMine)
                {
                    showMines();

                    gameCell.Background = (ImageBrush)btnStatesImg["btnBombedImg"];
                    gameCell.isOpened = true;
                    gameCell.Style = (Style)btnStatesImg["btnStyleOpened"];

                    minesWasOpened?.Invoke();

                    mineWasOpened = true;
                }
                else
                {
                    openCellWithoutMine(gameCell);

                    if (currentOpendCells == ((fieldHeigth * fieldWidth) - currentMinesAmount))
                    {
                        allNotMinedCellsIsOpened?.Invoke();
                    }

                    mineWasOpened = false;
                }
            }
        }
        private void rightClick(GameCell gameCell)
        {
            if (!gameCell.isOpened)
            {
                if (!gameCell.isFlaged && (currentFlagesAmount > 0))
                {
                    gameCell.Background = (ImageBrush)btnStatesImg["btnflagedImg"];
                    currentFlagesAmount--;
                    gameCell.isFlaged = true;

                } else if (gameCell.isFlaged && (currentFlagesAmount >= 0))
                {
                    gameCell.Background = (ImageBrush)btnStatesImg["btnClosedImg"];
                    currentFlagesAmount++;
                    gameCell.isFlaged = false;
                }

                flagAmountChanged?.Invoke();
                mineWasOpened = false;
            }
        }

        private void cell_MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            GameCell gameCell = (sender as GameCell);

            if (e.ChangedButton == MouseButton.Left)
            {
                leftClick(gameCell);
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                rightClick(gameCell);
            }

            if (!firstBtnClick && !mineWasOpened)
            {
                firstClickWas?.Invoke();
                firstBtnClick = true;
            }
        }
    }
}
