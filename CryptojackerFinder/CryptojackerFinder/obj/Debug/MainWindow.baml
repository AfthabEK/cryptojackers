   M S B A M L   `   `   `  ����  CryptojackerFinder#    CryptojackerFinder.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5        $
��Main��5        � Title$  CryptojackerFinder��5   	   $	��450��6$   $	��700��64   $	��990��6C   .��5      �  � RowDefinitions 5	   
   �� 5
      $��* �6   $	��100��6'   �� 5      $	��150 �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��380 �6   .+�5   
   &   System.Windows.Controls.DataGrid  $��15q�6M      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .    
IsReadOnly . ��6   ��  �� ElementName$
 Main�� �� Path$ 
Pretenders�	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 6h      Header$	 PID��6u   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	ProcessId+ �� UpdateSourceTrigger$ PropertyChanged=�6u     5      $

 auto 6j   $ Имя процесса��6w    6%   �� *ProcessName+$ PropertyChanged=�6w     5      $

 auto 6k   $
 ЦП��6x    6%   �� *CpuUsageView+$ PropertyChanged=�6x     5      $

 auto 6s   $D >Среднеквадратичное отклонение ЦП��6�    6%   �� *CpuStandardDeviation+$ PropertyChanged=�6�     5      $

 auto 6k   $ Память��6x    6%   �� *RamUsageView+$ PropertyChanged=�6x     5      $

 auto 6k   $ Сеть��6x    6%   �� *NetUsageView+$ PropertyChanged=�6x     5      $

 auto 6m   $ Доступ к CryptoAPI��6z    6%   �� *CryptoApiCalls+$ PropertyChanged=�6z      RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      ��  �� Property � �� �� Value
 �  ��63    �� Triggers 5       �� 5!       �� Binding 6&   �� *IsCryptoJacker+ �� Value true6I   .a�5"      ��  � ��
 � � �6;   5#      5%      5'      �� 5)   
   $��0��6   $��1��6"   $��30q�62   .+�5*      ��  �� Orientation$ 
Horizontal=�6   $��5q�62   .+�5+      �� $��50��6   {�5,      �� $
 Main��$ IsCanSetting�$ PropertyChanged=���5-      �� $
 Main��$ CpuUsage�$ PropertyChanged=��� 5.      $	��250��6   .��6$   /-Порог использования CPU (%)6A   5/      �� 50      $ 
Horizontal=�6   $��5q�62   .+�51      �� $��50��6   {�52      �� $
 Main��$ IsCanSetting�$ PropertyChanged=���53      �� $
 Main��$ CpuStandardDeviation�$ PropertyChanged=��� 54      $	��250��6   .��6$   @>Среднеквадратичное отклонение ЦП6F   55      �� 56      $ 
Horizontal=�6   $��5q�62   .+�57      �� $��50��6   {�58      �� $
 Main��$ IsCanSetting�$ PropertyChanged=���59      �� $
 Main��$ RamUsage�$ PropertyChanged=��� 5:      $	��250��6   .��6$   *(Потребление памяти (Mb)6=   5;      �� 5<      $ 
Horizontal=�6   $��5q�62   .+�5=      �� $��50��6   {�5>      �� $
 Main��$ IsCanSetting�$ PropertyChanged=���5?      �� $
 Main��$ NetUsage�$ PropertyChanged=��� 5@      $	��250��6   .��6$   A?Скорость передачи по сети (KBytes/minute)6O   5A      �� 5B      $ 
Horizontal=�6   $��5q�62   .+�5C      �� $��50��6   {�5D      �� $
 Main��$ IsCanSetting�$ PropertyChanged=���5E      �� $
 Main��$ CryptoApiCalls�$ PropertyChanged=��� 5F      $	��260��6   .��6$   FDКоличество обращений к CryptoApi в минуту6O   5G      5H      �� 5I   
   $��1��6   $��0��6"   $ 
Horizontal=�62   $��25��6K   $��20q�6W   .+�5J      �� $ ��Время анализа:��6   �� 5K      $��60��6   ��6!   �� $
 Main��$ AnalyzeTime� �� StringFormat$ {0:hh':'mm':'ss}��$ PropertyChanged=�{�5M      �� $
 Main��$ IsCanSetting�$ PropertyChanged=�6r   5N      �� 5O   
   $��1��6   $��1��6"   $��0,0,0,90q�62   $ 
Horizontal=�6D   $��Center=�6]   $��Center=�6z   .+�5P      �� $��25(�6   ��6&   �� $
 Main��$ CurrentTime�$ {0:hh':'mm':'ss}��$ PropertyChanged=��� 5R      $��25(�6   /6)   �� 5S      $��25(�6   ��6&   �� $
 Main��$ AnalyzeTime�$ {0:hh':'mm':'ss}��$ PropertyChanged=�5U      �� 5V   
   -   5        $��StartButton��5W      $��1��5V      $��1��6   $��50��6.   $	��170��6:   $��
Старт��6F   5W   &   �� 5X   
   -   5        $��
StopButton��5Y      $��1��5X      $��1��6   $��50��6.   $	��170��6:   $��Стоп��6F   $y�Hidden=�5Y   #   69   5Z      5[      