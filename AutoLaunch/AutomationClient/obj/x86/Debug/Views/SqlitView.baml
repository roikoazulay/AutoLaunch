   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   &     AutomationClient.Views.SqlitView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Sqlite��5   	   $	��485��5   	   $	��223��5   	    � Icon$  /Images/data-redo-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      .+�5'      �� $��0��6   $
��Left=�5(      $��DB Name:��5)      �� 5*      -   5        $��	dbNameTxb��5*      $��0��5+      $��1��5,      $	��250��5-      $
��Left=�5.      �� 50      $��1��6   $
��Left=�51      $��Options:��52      �� 53      -   5        $��
optionsTxb��53      $��1��54      $��1��55      $	��250��56      $
��Left=�57      �� 59      $��2��6   $
��Left=�5:      $��Query:��5;      �� 5<      -   5        $��queryTxb��5<      $��2��5=      $��1��5>      $	��250��5?      $
��Left=�5@      
 �� 
IsEditable
 . ��5B      ��  �� Path$ VariableList��� 5C      $��3��6   $
��Left=�5D      $��Traget Variable:��5E      �� 5F      -   5        $��varCmb��5F      $��3��5G      $��1��5H      $	��250��5I      $
��Left=�5J      
 . ��5L      �� $ VariableList��� 5N      $��4��6   $
��Left=�5O      $��Action:��5P      �� 5Q      -   5        $��operationCmb��5Q      $��4��5R      $��1��5S      $	��250��5T      $
��Left=�5U      
 .  �� 
IsReadOnly .  �� Text$ EexcuteQuery��5X      5Y      5Z      5[      