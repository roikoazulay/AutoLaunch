   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   1    +AutomationClient.Views.DictionaryActionView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	   -   5          � Title$  
Dictionary��5   	   $	��484��5   	   $	��266��5   	    � Icon$* $/Images/Dictionary-Mac-Book-icon.png��5   	    � 
ResizeMode$ NoResize=�5
   	   .��5      �� $	��228��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
    � ColumnDefinitions 5      �� 5      $	��96* �6#   �� 5      $
��415* �6#   	 � RowDefinitions	 5      �� 5       $��30 �6    �� 5!      $��30 �6    �� 5"      $��30 �6    �� 5#      $��31 �6    �� 5$      $��29 �6    �� 5%      $��34 �6    .+�5'      �� $��2��6   $��Center=�5(      $��Value:��5)      �� 5*      -   5        $��valueCmb��5*      $��2��5+      $��1��5,      $	��370��5-      $
��Left=�5.      
 �� 
IsEditable
 . ��50      ��  �� Path$ VariableList��� 51      $��3��6   $��Center=�52      $��Action:��53      �� 54      -   5        $��operationCmb��54      $��3��55      $��1��56      $	��370��57      $��0,1,0,0q�58      $
��Left=�59      
 .  �� 
IsReadOnly .  �� Text$ Create��5>      ��5=      �� $ DictionaryOperations�5>      �� 5@      $��4��6   $��Center=�5A      $��Target:��5B      �� 5C      -   5        $��targetVarCmb��5C      $��4��5D      $��1��5E      $	��370��5F      $
��Left=�5G      
 . ��5I      �� $ VariableList��� 5J      $��5��6   $��Center=�5K      $��
File Name:��5L      �� 5M      -   5        $��fileNameCmb��5M      $��5��5N      $��1��5O      $	��370��5P      $��0,2,0,3q�5Q      $
��Left=�5R      
 . ��5T      �� $ VariableList��� 5U      $��1��6   $
��Key:��6!   �� 5V      -   5        $��keyCmb��5V      $��1��5W      $��1��5X      $	��370��5Y      $
��Left=�5Z      
 . ��5\      �� $ VariableList��� 5]      $��Name:��6   �� 5^      -   5        $��dictionaryName��5^      $��1��5_      $	��370��5`      $
��Left=�5a      5b      5c      5d      