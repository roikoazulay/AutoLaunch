   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   *    $AutomationClient.Views.ConditionView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  	Condition��5   	   $	��299��5   	   $	��160��5   	    � Icon$* $/Images/Document-Flow-Chart-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      �� $��If Variable:��6   �� 5      -   5        $��varCmb��5      $	��134��5      $��23��5      $��7,0,0,0q�5      	 �� 
IsEditable	 . ��5       �� 
 �� Path$
 VariableList�5!      �� 5#   
   $ 
Horizontal=�6   .+�5$      �� $��	Is Equal:��6   �� 5%      -   5        $��conditionCmb��5%      $	��134��5&      $��19,0,0,0q�5'      	 .  �� Text$
 True��5)      .@�5*      �� $
��True��6   �� 5+      $��False��6   5,      5-      �� 5.   
   $ 
Horizontal=�6   .+�5/      �� $��GoTo Lable:��6   �� 50      -   5        $��lableCmb��50      $	��134��6&   51      52      53      