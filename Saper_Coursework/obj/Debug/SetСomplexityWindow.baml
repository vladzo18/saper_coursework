   M S B A M L   `   `   `  ����K  GSaper_Coursework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Saper_CourseworkSaper_Coursework  5   	   +    %Saper_Coursework.SetСomplexityWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Saper_Coursework     5   	     � Title$#  Своя складність��5   	   $	��251��6!   $	��300��6.    � WindowStartupLocation$ CenterOwner=�5	   	    � 
ResizeMode$ NoResize=�6-    � Icon$ .\img\icon.ico��5
   	   .��5      � ���#F8F1FF � RowDefinitions 5   
   �� 5      $��42 �6   �� 5      $��14 �6   �� 5      $��55 �6   �� 5      $��56 �6   �� 5      $��55 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��30��6   $��0��6   $��0��6,   $
��0,13q�69   $��2��6G   .��6X   Довжина поля:6g   �� 5   
   -   5         ��Name$ 	heightBox��5      $��30��6%   $��1��61   $��0��6A   $
��0,13q�6N   $��2��6\   6o   �� 5   
   $��30��6   $��0��6   $��2��6,   $��	0,12,0,13q�69   .��6L   Ширина поля:6Z   �� 5   
   -   5        $ widthBox��5      $��30��6$   $��1��60   $��2��6@   $��	0,12,0,13q�6M   6b   �� 5   
   $��30��6   $��0��6   $��3��6,   $
��0,13q�69   .��6G   Кількість мін:6W   �� 5   
   -   5        $ minesAmountBox��5      $��30��6*   $��1��66   $��3��6F   $
��0,13q�6S   6c   �� 5    
   -   5        $��Почати гру��5       $��0��5!      $��4��6!   $	��100��5"      $��30��6   �� 5#   
   -   5        $��Відміна��5#      $��1��5$      $��4��6!   $	��100��5%      $��30��6   5&      5'      