   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   1    +AutomationClient.Views.TextManipulationView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Text Operations��5   	   $	��440��5   	   $	��360��5   	    � Icon$  /Images/Text-File-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��412��6   $	��275��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       $��30 �6    �� 5!      $��30 �6    �� 5"      $	��150 �6    �� 5#      $��30 �6    �� 5$      $��30 �6    .+�5&      �� $��0��6   $��Center=�5'      $��Variable Name:��5(      �� 5)      -   5        $��srcCmb��5)      $��1��5*      $	��300��5+      $
��Left=�5,      
 �� 
IsEditable
 . ��5.      ��  �� Path$ VariableList��� 50      $��2��6   $��0��51      $��Center=�52      $��Value:��53      �� 54      -   5        $��valueTxb��54      $��2��55      $��1��56      $	��300��57      $
��Left=�58       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5:      " �� VerticalScrollBarVisibility$ Visible=�5;      �� 5<      $��1��6   $��Action:��6!   �� 5=      -   5        $��operationCmb��5=      $��1��5>      $��1��5?      $	��300��5@      $
��Left=�5A      
 .  �� 
IsReadOnly .  �� Text$ Contains��5F      ��5E      �� $ TextOperations�5F      �� 5G      $��3��6   $��0��5H      $��Center=�5I      $��Length:��5J      �� 5K      -   5        $��	lengthTxb��5K      $��3��5L      $��1��5M      $	��300��5N      $
��Left=�5O       . {�.  $
 Wrap=�5R      $ Visible=�5S      �� 5U      $��4��6   $��Target Variable:��6!   �� 5V      -   5        $��	targetCmb��5V      $��5��5W      $��1��5X      $	��300��5Y      $
��Left=�5Z      
 . ��5\      �� $ VariableList�5]      5^      5_      