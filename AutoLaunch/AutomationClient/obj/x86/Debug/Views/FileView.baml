   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   %    AutomationClient.Views.FileView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  	File Info��5   	   $	��381��5   	   $	��157��5   	    � Icon$ /Images/file-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $	��360��6   $	��123��5      
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��340��6   $��79��6    � ColumnDefinitions 5      �� 5      $	��93* �6#   �� 5      $
��247* �6#   	 � RowDefinitions	 5!      �� 5"      �� 5#      �� 5$      .+�5'      �� $
��Left=�6   $��
File Name:��6/   �� 5(      -   5        $��fileNameCmb��5(      $��1��5)      $	��230��5*      $
��Left=�5+      
 �� 
IsEditable
 . ��5-      ��  �� Path$ VariableList��� 5.      -   5        $��valueLbl��5.      $��1��5/      $��2��50      $��29��51      $
��Left=�52      $	��Top=�53      $��Target Variable:��54      �� 55      -   5        $��valueCmb��55      $��1��56      $��1��57      $	��230��58      $��26��59      $
��Left=�5:      $	��Top=�5;      
 . ��5=      �� $ VariableList��� 5I      $��2��6   $��0,0,44,0q�5J      $��Action:��5K      �� 5L      -   5        $��operationCmb��5L      $��2��5M      $��1��5N      $	��230��5O      $
��Left=�5P      
 .  �� 
IsReadOnly .  �� Text$ GetFileLength��5U      ��5T      �� $ FileOperations�5U      5V      5W      5X      