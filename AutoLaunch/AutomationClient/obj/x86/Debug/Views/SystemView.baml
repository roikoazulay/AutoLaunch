   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   '    !AutomationClient.Views.SystemView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  System��5   	   $	��381��5   	   $	��136��5   	    � Icon$) #/Images/Actions-system-run-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $	��101��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��357��6   $��59��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      .+�5#      �� $��0��6   $��Action:��6!   �� 5$      -   5        $��operationCmb��5$      $��0��5%      $��1��5&      $	��250��5'      $
��Left=�5(      
 �� 
IsEditable
 .  �� 
IsReadOnly .  �� Text$ GetSuiteStatus��5,      ��5+      ��  �� Path$ SystemOperations�5,      �� 5.      $��1��6   $��Target Variable:��6!   �� 5/      -   5        $��	targetCmb��5/      $��1��50      $��1��51      $	��250��52      $
��Left=�53      
 . ��55      �� $ VariableList�56      57      58      