   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   '    !AutomationClient.Views.Hr80PSView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   0localclr-namespace:AutomationClient     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  HR80 Power Supply��5   	   $	��384��5   	    � Icon$ /Images/rsz_hr80ps.png��5
   	    � 
ResizeMode$ NoResize=�5   	    � SizeToContent$ Height=�5   	   .��5      �� $	��363��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   	 � ColumnDefinitions	 5       �� 5!      $	��97* �6#   �� 5"      $
��271* �6#   
 � RowDefinitions
 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      .+�5+      �� $��
Host Name:��6   �� 5,      -   5        $��hostCmb��5,      $��1��5-      $	��250��5.      $
��Left=�5/       �� 
IsEditable . ��51      ��  �� Path$ VariableList��� 53      $��1��6   $��Port:��6!   �� 54      -   5        $��portTxb��54      $��1��55      $��1��56      $	��250��57      $
��Left=�58      $
��4001��59      �� 5:      $��2��6   $��Action:��6!   �� 5;      -   5        $��operationCmb��5;      $��2��5<      $��1��5=      $	��250��5>      $
��Left=�5?       .  �� 
IsReadOnly .  �� Text$ Connect��5C      ��5B      �� $ HR80Operations�5C      �� 5E      $��3��6   $
��Volt��6!   �� 5F      -   5        $��	param1Txb��5F      $��3��5G      $��1��5H      $	��250��5I      $
��Left=�5J       . ��5L      �� $ VariableList��� 5N      $��4��6   $��Target Varible��6!   �� 5O      -   5        $��targetVarCmb��5O      $��4��5P      $��1��5Q      $	��250��5R      $
��Left=�5S       . ��5U      �� $ VariableList�5V      5W      5X      