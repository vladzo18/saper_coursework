   M S B A M L   `   `   `  ���� K  GSaper_Coursework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Saper_CourseworkSaper_Coursework  !    Saper_Coursework.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:Saper_Coursework     -     � Title$  	Saper 1.0�� � 
ResizeMode$ NoResize=� � Icon$ .\img\icon.ico�� � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterScreen=�.���� -    ��Name$ 
stackPanel��
�������.+�o� -    o�Name$ mainMenu��$��20��.@�m� $��Гра��.@�m� -   $��Нова гра��$��20��m� -   $��
Пауза��$��20��m� $��Завершити гру��$��20��m� $��Складність��.@�m� -    m�Name$ easyMenuItem��$��Новачок��$��20�� m� IsCheckable . 	 m� 	IsChecked	 . m� -   $ mediumMenuItem��$��Продвинутий��$��20�� . m� -   $ hardMenuItem��$��
Профі��$��20�� . m� -	   $#��Своя складність��$��20��m� -
   $��Рейтинг��$��20��� -   
 �Name$
 	infoPanel��$��30��$	��480�����#BEBEBE$��Stretch=� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $��labelMinesAmount��$��Showcard Gothic)�$��Кількіть мін: ��$��0��$��14(�$
��Bold"����#50B2C0�� -   $��labelFlagesAmount��$��Showcard Gothic)�$+��%Кількіть прапорців: ��$��1��$��14(�$
��Bold"����#50B2C0�� -   $��	labelTime��$��Showcard Gothic)�$��00:00:00��$��3��$��14(�$
��Bold"����#BA1200�� -   $��userNameLabel��$��Showcard Gothic)�$��Гравець: ��$��2��$��14(�$
��Bold"����#201E1F�� -    ��Name$ 	gameField��
������ 