   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   '    !AutomationClient.Views.TelnetView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Telnet��5   	   $	��391��5   	   $	��231��5   	    � Icon$' !/Images/Computer-Monitor-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $
�� 150��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      .+�5&      �� $��0��6   $��0��5'      $
��Left=�5(      $��Host:��5)      �� 5*      -   5        $��hostTxb��5*      $��0��5+      $��1��5,      $	��250��5-      $
��Left=�5.      
 �� 
IsEditable
 . ��50      ��  �� Path$ VariableList��� 51      $��1��6   $��0��52      $
��Left=�53      $��Port:��54      �� 55      -   5        $��portTxb��55      $��1��56      $��1��57      $	��250��58      $
��Left=�59      $��23��5:      �� 5<      $��2��6   $��0��5=      $
��Left=�5>      $��Command:��5?      �� 5@      -   5        $��
commandTxb��5@      $��2��5A      $��1��5B      $	��250��5C      $
��Left=�5D      �� 5F      $��3��6   $��0��5G      $
��Left=�5H      $��Traget Variable:��5I      �� 5J      -   5        $��varCmb��5J      $��3��5K      $��1��5L      $	��250��5M      $
��Left=�5N      
 . ��5P      �� $ VariableList��� 5R      $��4��6   $��0��5S      $��Action:��5T      �� 5U      -   5        $��operationCmb��5U      $��4��5V      $��1��5W      $	��250��5X      $
��Left=�5Y      
 .  �� 
IsReadOnly .  �� Text$ Connect��5]      ��5\      �� $ TelnetOperations�5]      5^      5_      5`      