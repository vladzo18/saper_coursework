   M S B A M L   `   `   `  ����K  GSaper_Coursework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Saper_CourseworkSaper_Coursework  5   	   !    Saper_Coursework.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Saper_Coursework     5   	   -   5          � Title$  	Saper 1.0��5   	    � 
ResizeMode$ NoResize=�6    � Icon$ .\img\icon.ico��5	        � SizeToContent$ WidthAndHeight=�5   	    � WindowStartupLocation$ CenterScreen=�6(   .��5      �� -   5         ��Name$ 
stackPanel��5   (   
�������.+�5   
   o� -   5         o�Name$ mainMenu��5      $��20��6   .@�5      m� $��Гра��6   .@�5      m� -   5        $��Нова гра��5      $��20��6-   6R   m� 5      -   5        $��
Пауза��5      $��20��6*   6O   m� 5      -   5        $��Завершити гру��5      $��20��62   6V   5      m� 5      $��Складність��6   .@�5      m� -   5         m�Name$ easyMenuItem��5      $��Новачок��5      $��20��6,    m� IsCheckable . 	 m� 	IsChecked	 . 5   3   m� 5      -   5        $ mediumMenuItem��5   4   $��Любитель��5      $��20��6-    . 5   N   m� 5      -	   5        $ hardMenuItem��5   2   $��Майстер��5      $��20��6,    . 5   J   m� 5      -
   5        $#��Своя складність��5      $��20��64   5   4   5      m� 5      -   5        $��Рейтинг��5      $��20��6(   64   5      � 5    
   -   5        
 �Name$
 	infoPanel��5    '   $��30��6   $	��480��6   ���#BEBEBE$��Stretch=�5!   $    � ColumnDefinitions 5"      �� 5#      $
��Auto �6#   �� 5$      $
��Auto �6#   �� 5%      $
��Auto �6#   �� 5&      $
��Auto �6#   .+�5(      �� -   5        $��labelMinesAmount��5(      $��Showcard Gothic)�6,   $��Кількіть мін: ��5)      $��0��6-   $��14(�5*      $
��Bold"�6"   ���#50B2C064   �� 5+      -   5        $��labelFlagesAmount��5+      $��Showcard Gothic)�6-   $+��%Кількіть прапорців: ��5,      $��1��63   $��14(�5-      $
��Bold"�6"   ���#50B2C064   �� 5.      -   5        $��	labelTime��5.      $��Showcard Gothic)�6%   $��00:00:00��5/      $��3��6'   $��14(�50      $
��Bold"�6"   ���#BA120064   �� 51      -   5        $��userNameLabel��51      $��Showcard Gothic)�6)   $��Гравець: ��52      $��2��6(   $��14(�53      $
��Bold"�6"   ���#201E1F64   54      �� 55   
   -   5         ��Name$ 	gameField��55   *   
������ 56      57      