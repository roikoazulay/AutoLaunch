   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   $    AutomationClient.Views.SshView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$	  SSH��5   	   $	��399��5   	   $	��242��5   	    � Icon$. (/Images/Apps-utilities-terminal-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��163��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      .+�5'      �� $��Host:��6   �� 5(      -   5        $��varCmb��5(      $��0��5)      $��1��5*      $	��250��5+      $
��Left=�5,      
 �� 
IsEditable
 . ��5/      ��  �� Path$ VariableList��� 51      $��1��6   $��
User Name:��6!   �� 52      -   5        $��userNameTxb��52      $��1��53      $��1��54      $	��250��55      $
��Left=�56      $
��root��57      �� 59      $��2��6   $��	Password:��6!   �� 5:      -   5        $��passwordTxb��5:      $��2��5;      $��1��5<      $	��250��5=      $
��Left=�5>      $��ortech��5?      �� 5A      $��3��6   $��Command:��6!   �� 5B      -   5        $��
commandCmb��5B      $��3��5C      $��1��5D      $	��250��5E      $
��Left=�5F      
 . ��5H      �� $ VariableList��� 5J      $��4��6   $��Action:��6!   �� 5K      -   5        $��operationCmb��5K      $��4��5L      $��1��5M      $	��250��5N      $
��Left=�5O      
 .  �� 
IsReadOnly .  �� Text$ Connect��5S      ��5R      �� $ SshOperations�5S      �� 5U      $��5��6   $��	Log File:��6!   �� 5V      -   5        $��logCmb��5V      $��5��5W      $��1��5X      $	��250��5Y      $
��Left=�5Z      
 . ��5\      �� $ VariableList�5]      5^      5_      