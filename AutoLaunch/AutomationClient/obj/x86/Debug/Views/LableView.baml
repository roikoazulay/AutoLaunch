   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   &     AutomationClient.Views.LableView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Lables��5   	   $	��381��5   	   $	��168��5   	    � Icon$ /Images/Bookmark-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $��85��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      .+�5$      �� $��Lable Name:��6   �� 5%      -   5        $��lableCmb��5%      $��0��5&      $��1��5'      $	��250��5(      $
��Left=�5)      �� 5+      $��1��6   $��Action:��6!   �� 5,      -   5        $��operationCmb��5,      $��1��5-      $��1��5.      $	��250��5/      $
��Left=�50      
 �� 
IsEditable
 .  �� 
IsReadOnly .  �� Text$ Create��55      ��54      ��  �� Path$ LableOperations�55      �� 57      $��2��6   $��Loop #��6!   �� 58      -   5        $��loopCountCmb��58      $��2��59      $��1��5:      $	��250��5;      $
��Left=�5<      
 . $ 1��5?      ��5>      �� $ VariableList�5?      5@      5A      5B      