   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   '    !AutomationClient.Views.TimersView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Icon$  /Images/Timer-icon.png��5   	    � Title$ Timer��5   	   $	��173��6   $	��371��6$    � 
ResizeMode$ NoResize=�60    � WindowStartupLocation$ CenterScreen=�6F   .��5      �� 
�������$	��147��6*   .+�5   
   c� $��41��6   .@�5	      �� -   5        $��saveBtn��5	      ��5
      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��100��6   	 � ColumnDefinitions	 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   
 � RowDefinitions
 5      �� 5      �� 5      �� 5      .+�5       �� $��Timer Name:��6   $��0��6*   $��0��67   �� 5!      -   5        $��timerNameTxb��5!      $	��250��6+   $��0��67   $��1��6D   $
��Left=�6T   �� 5"      $��Action:��6   $��1��6&   $��0��63   �� 5#      -   5        $��operationCmb��5#   w    �� 
IsReadOnly .  �� 
IsEditable .  �� Text$ Create��6i   $	��250��6�   $��1��6�   $��1��6�   $
��Left=�6�   ��6<   ��  �� Path$ TimerOperations�6�   �� 5%      $��Target Variable:��6   $��2��60   $��0��6=   �� 5&      -   5        $��targetVarCmb��5&   T    . $	��250��6h   $��2��6t   $��1��6�   $
��Left=�6�   ��6)   �� $ VariableList�6�   5'      �� 5)   
    �� Orientation$ 
Horizontal=�6   5*      5+      5,      