   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   -    'AutomationClient.Views.RelayControlView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Relay Control��5   	   $	��361��5   	   $	��182��5   	    � Icon$( "/Images/Style-Switch-User-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $	��155��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��110��6    � ColumnDefinitions 5      �� 5      $	��76* �6#   �� 5      $
��225* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      �� 5#      .+�5&      �� $
��Left=�6   $��	COM Port:��6/   �� 5'      -   5        $��portCmb��5'      $��1��5(      $	��200��5)      $
��Left=�5*      
 �� 
IsEditable
 . ��5,      ��  �� Path$ VariableList��� 5.      $��1��6   $
��Left=�5/      $��Vendor:��50      �� 51      -   5        $��	vendorCmb��51      $��1��52      $��1��53      $	��200��54      $
��Left=�55      
 .  �� 
IsReadOnly .  �� Text$ KMTRONIC8PORT��58      .@�59      �� $��KMTRONIC8PORT��6   �� 5:      $��IA3174_32PORT��6   5;      �� 5<      $��2��6   $
��Left=�5=      $��Relay Port:��5>      �� 5?      -   5        $��relayNumberCmb��5?      $��2��5@      $��1��5A      $	��200��5B      $
��Left=�5C      
 . ��5E      �� $ VariableList��� 5F      $��3��6   $��Action:��6!   �� 5G      -   5        $��operationCmb��5G      $��3��5H      $��1��5I      $	��200��5J      $
��Left=�5K      
 .  . $ Connect��5P      ��5O      �� $ RelayControlOperations�5P      5Q      5R      5S      