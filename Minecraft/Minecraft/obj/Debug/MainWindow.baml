   M S B A M L   `   `   `  ����D  @Minecraft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Minecraft	Minecraft  5   	   =#clr-namespace:Minecraft.ControllersMinecraft.Controllers  5   	       Minecraft.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Minecraft     5   	   6local1#clr-namespace:Minecraft.Controllers     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    � 	Resources 5
      '   !Minecraft.Controllers.MyConverter   MyConverter��%   &	          5   
   6   5      .��5      � .+�5   
   �� $��1q�6   
���   �._�5      �  � RowDefinitions 5      �� 5      $	��10* �6$   �� 5      $��1* �6$    � ColumnDefinitions 5      �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5   /   $��2��6      CanUserAddRows .     AutoGenerateColumns .  $
��Left=�5    1   $	��Top=�6L      
IsReadOnly .  $��2��6�   $��
12,178,0,0q�6�   ��5   X   �� *	MobList+ �� SelectedItem 5    w   �� *SelectedMob+	   Columns	 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Название моба��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *	MobName+  5#      $
 Здоровье��6-    6?   �� *	MobHealth+  5$      $
 
Спавн��6-    6<   �� *
Location+ �� 	Converter# [    5%      $
 Дроп��6-    6;   �� *Drop+# [  5'      �� 5)      -   5        $��StartBtn��5)      $��0��6)   $��5��69   $��Загрузить��6F   ��5*      �� 5+       �� Path$ StartParsingCommand�6"    �� Mode$ OneWay=�6=   5-      �� 5/      -   5        $��DeleteSelected��5/      $��2��6/   $��1��6?   $!��Удалить запись��6L   ��6e   �� *DeleteSelectedCommand+�� 50      -   5        $��	DeleteAll��50      $��3��6*   $��1��6:   $(��"Удалить все записи��6G   ��6d   �� *DeleteAllCommand+52      53      54      55      