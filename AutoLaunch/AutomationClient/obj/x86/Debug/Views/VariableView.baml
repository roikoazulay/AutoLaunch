   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   )    #AutomationClient.Views.VariableView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Variables Operations��5   	   $	��533��5   	   $	��354��5   	    � Icon$ /Images/FileHash.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5      $��30 �6    �� 5       $	��145 �6    �� 5!      $��30 �6    �� 5"      $��30 �6    �� 5#      $��30 �6    .+�5%      �� $��0��6   $��Center=�5&      $��Variable Name:��5'      �� 5(      -   5        $��varCmb��5(      $��1��5)      $	��370��5*      $
��Left=�5+      
 �� 
IsEditable
 . ��5-      ��  �� Path$ VariableList�5/      �� 51      $��1��6   $��Center=�52      $��Value:��53      �� 54      -   5        $��valueTxb��54      $��1��55      $��1��56      $	��370��57      $
��Left=�58       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5:      " �� VerticalScrollBarVisibility$ Visible=�5;      �� 5<      $��2��6   $��Center=�5=      $��Action:��5>      �� 5?      -   5        $��operationCmb��5?      $��2��5@      $��1��5A      $	��370��5B      $
��Left=�5C      
 .  �� 
IsReadOnly .  �� Text$ Create��5H      ��5G      �� $ VariableOperations� � ToolTip 5J      _� 5K      .��5L      �� -   5        $��tipTool��5L   $   ��63   �� *description+5M      5O      �� 5Q      $��3��6   $��Center=�5R      $��Result Target:��5S      �� 5T      -   5        $��targetVarCmb��5T      $��3��5U      $��1��5V      $	��370��5W      $
��Left=�5X      
 . ��5Z      �� $ VariableList��� 5\      $��4��6   $��Center=�5]      $��
File Name:��5^      �� 5_      -   5        $��fileCmb��5_      $��4��5`      $��1��5a      $	��250��5b      $��23��5c      $
��Left=�5d      $��Center=�5e      
 . ��5g      �� $ VariableList��� 5i      -   5        $��fileNameCmb��5i      $��4��5j      $��1��5k      $	��370��5l      $
��Left=�5m      
 . ��5o      �� $ VariableList�5p      5q      5r      