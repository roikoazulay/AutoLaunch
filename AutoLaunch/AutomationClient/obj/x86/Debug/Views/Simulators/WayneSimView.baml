   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   )    #AutomationClient.Views.WayneSimView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Wayne Simulator��5   	   $	��406��5   	   $	��175��5   	    � Icon$$ /Images/scuSIM_PS_MAINICON.ico��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $��94��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5      �� 5       �� 5!      �� 5"      .+�5%      �� $��Target Variable:��6   �� 5&      -   5        $��varCmb��5&      $��0��5'      $��1��5(      $	��250��5)      $
��Left=�5*      
 �� 
IsEditable
 . ��5,      ��  �� Path$ VariableList��� 5.      $��1��6   $��Pump Number:��6!   �� 5/      -   5        $��pumpCmb��5/      $��1��50      $��1��51      $	��250��52      $
��Left=�53      
 .  �� 
IsReadOnly .  �� Text$ 0��56      .@�57      �� $��0��6   �� 58      $��1��6   59      �� 5:      $��2��6   $��Nzl Number:��6!   �� 5;      -   5        $��nzlCmb��5;      $��2��5<      $��1��5=      $	��250��5>      $
��Left=�5?      
 .  . $ 1��5B      .@�5C      �� $��1��6   �� 5D      $��2��6   �� 5E      $��3��6   �� 5F      $��4��6   �� 5G      $��5��6   �� 5H      $��6��6   �� 5I      $��7��6   �� 5J      $��8��6   5K      �� 5L      $��4��6   $��Action:��6!   �� 5M      -   5        $��operationCmb��5M      $��4��5N      $��1��5O      $	��250��5P      $
��Left=�5Q      
 .  . $ HandleUp��5U      ��5T      �� $ WayneOperations�5U      5V      5W      5X      