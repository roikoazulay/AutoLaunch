   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   %    AutomationClient.Views.AutoItUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  UI Operations��5   	   $	��403��5   	   $	��217��5   	    � Icon$  /Images/Text-File-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��137��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      .+�5&      �� $��0��6   $��Center=�5'      $��Window Title:��5(      �� 5)      -   5        $��winTitleCmb��5)      $��1��5*      $	��250��5+      $
��Left=�5,      
 �� 
IsEditable
 . ��5.      ��  �� Path$ VariableList��� 50      $��1��6   $��0��51      $��Center=�52      $��Control Id:��53      �� 54      -   5        $��controlIdTxb��54      $��1��55      $��1��56      $	��250��57      $
��Left=�58       �� AcceptsReturn . 59      �� 5;      $��2��6   $��Text:��6!   �� 5<      -   5        $��textCmb��5<      $��2��5=      $��1��5>      $	��250��5?      $
��Left=�5@      
 . ��5B      �� $ VariableList��� 5D      $��3��6   $��Target Varible:��6!   �� 5E      -   5        $��	targetCmb��5E      $��3��5F      $��1��5G      $	��250��5H      $
��Left=�5I      
 . ��5K      �� $ VariableList��� 5M      $��4��6   $��Action:��6!   �� 5N      -   5        $��operationCmb��5N      $��4��5O      $��1��5P      $	��250��5Q      $
��Left=�5R      
 .  �� 
IsReadOnly .  �� Text$ ControlClick��5V      ��5U      �� $ AutoItGuiOperations�5V      5W      5X      5Y      