   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   /    )AutomationClient.Views.MotorControlerView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Motor Controller��5   	   $	��361��5   	   $	��157��5   	    � Icon$( "/Images/Style-Switch-User-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $	��123��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��332��6   $��79��6    � ColumnDefinitions 5      �� 5      $	��84* �6#   �� 5      $
��200* �6#   �� 5      $	��48* �6#   	 � RowDefinitions	 5       �� 5!      �� 5"      �� 5#      .+�5&      �� $
��Left=�6   $��	COM Port:��6/   �� 5'      -   5        $��portCmb��5'      $��1��5(      $	��200��5)      $
��Left=�5*      
 �� 
IsEditable
 . ��5,      ��  �� Path$ VariableList��� 5-      $��1��6   $��29��5.      $
��Left=�5/      $	��Top=�50      $��Value:��51      �� 52      -   5        $��valueCmb��52      $��1��53      $��1��54      $	��200��55      $
��Left=�56      
 . ��58      �� $ VariableList��� 59      -   5        $��	infoImage��59      $��1��5:      $��2��5;      $��20��5<      $��20��5=      $��/Images/infoPic.png��5>       � ToolTip 5?      _� 5@       _� 	StaysOpen . .��6/   Set length (mm)6@   5B      �� 5N      $��2��6   $��Action:��6!   �� 5O      -   5        $��operationCmb��5O      $��2��5P      $��1��5Q      $	��200��5R      $
��Left=�5S      
 .  �� 
IsReadOnly .  �� Text$ Connect��5X      ��5W      �� $ MotorOperations�5X      5Y      5Z      5[      