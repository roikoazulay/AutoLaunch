   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   )    #AutomationClient.Views.DateTimeView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  	Date Time��5   	   $	��391��5   	   $	��213��5   	    � Icon$3 -/Images/Apps-preferences-system-time-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      �� $��0,0,0,0q�6   $��Source Variable:��6%   �� 5      -   5        $��	srcVarCmb��5      $	��200��5      $��23��5      $��2,0,0,0q�5      	 �� 
IsEditable	 . ��5       �� 
 �� Path$
 VariableList�5!      �� 5"   
   $ 
Horizontal=�6   .+�5#      �� $��0,0,53,0q�6   $��Value:��6&   �� 5$      -   5        $��valueCmb��5$      $	��200��5%      $��23��5&      $��2,0,0,0q�5'      	 . ��5)      �� $
 VariableList�5*      �� 5+   
   $ 
Horizontal=�6   .+�5,      �� $��0,0,16,0q�6   $��Time Format:��6&   �� 5-      -   5        $��timeFormatCmb��5-      $	��200��5.      $��23��5/      $��2,0,0,0q�50      	 .  �� Text$ yy-MM-dd HH:mm:ss.fff��52      .@�53      �� $��yyyy-MM-dd HH:mm:ss��6   �� 54      $��yy-MM-dd HH:mm:ss.fff��6   �� 55      $"��dddd, MMMM dd, yyyy HH:mm:ss��6   �� 56      $��HH:mm:ss��6   �� 57      $��HH:mm��6   �� 58      $��MM/dd/yyyy HH:mm��6   �� 59      $��dddd, MMMM dd, yyyy HH:mm��6   �� 5:      $��dddd, MMMM dd, yyyy��6   �� 5;      $��
MM/dd/yyyy��6   �� 5<      $��
dd/MM/yyyy��6   5=      5>      �� 5@   
   $ 
Horizontal=�6   .+�5A      �� $��0,0,2,0q�6   $��Traget Variable:��6%   �� 5B      -   5        $��	trgVarCmb��5B      $	��200��5C      $��23��5D      $��2,0,0,0q�5E      	 . ��5G      �� $
 VariableList�5H      �� 5I   
   $��0,2,0,0q�6   $ 
Horizontal=�6&   .+�5J      �� $��0,0,10,0q�6   $��Action:��6&   �� 5K      -   5        $��operationCmb��5K      $	��200��5L      $��40,0,0,0q�5M      	 .  �� 
IsReadOnly . $ GetCurrentTime��5Q      ��5P      �� $
 DateTimeOperations�5Q      5R      5S      5T      