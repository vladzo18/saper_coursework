   M S B A M L   `   `   `  ����K  GSaper_Coursework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Saper_CourseworkSaper_Coursework  5   	   #    Saper_Coursework.RatingWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Saper_Coursework     5   	     � Title$  Рейтинг��5   	   $	��200��6   $	��400��6&    � 
ResizeMode$ NoResize=�62    � Icon$ .\img\icon.ico��5	   	    � WindowStartupLocation$ CenterOwner=�6   .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5         AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Гравець��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 140 66   	   SortMemberPath$
	 Time��6d   
   CanUserSort
 . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   ��  �� Path$ userName�6z   5  /System.Windows.Controls.DataGridHyperlinkColumn  5      $ Складність��6*   $	 140 6>   
 .   6J   �� $ 
Complexity�6n     5      $ Час��6%   $	 100 62   
 .   6>   �� $
 Time�6\   5      5      5      