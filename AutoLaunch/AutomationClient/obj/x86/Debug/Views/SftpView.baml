   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   %    AutomationClient.Views.SftpView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Secured FTP��5   	   $	��391��5   	   $	��235��5   	    � Icon$&  /Images/Filesystems-ftp-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $	��209��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��163��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      .+�5(      �� $��Host:��6   �� 5)      -   5        $��varCmb��5)      $��0��5*      $��1��5+      $	��250��5,      $
��Left=�5-      
 �� 
IsEditable
 . ��5/      ��  �� Path$ VariableList��� 51      $��1��6   $��
User Name:��6!   �� 52      -   5        $��userNameTxb��52      $��1��53      $��1��54      $	��250��55      $
��Left=�56      $
��root��57      �� 59      $��2��6   $��	Password:��6!   �� 5:      -   5        $��passwordTxb��5:      $��2��5;      $��1��5<      $	��250��5=      $
��Left=�5>      $��ortech��5?      �� 5A      $��3��6   $��File or Folder:��6!   �� 5C      -   5        $��command1Txb��5C      $��3��5D      $��1��5E      $	��250��5F      $
��Left=�5G      
 . ��5I      �� $ VariableList��� 5K      $��4��6   $��Target Folder:��6!   �� 5L      -   5        $��command2Txb��5L      $��4��5M      $��1��5N      $	��250��5O      $
��Left=�5P      
 . ��5R      �� $ VariableList��� 5T      $��5��6   $��Action:��6!   �� 5U      -   5        $��operationCmb��5U      $��5��5V      $��1��5W      $	��250��5X      $
��Left=�5Y      
 .  �� 
IsReadOnly .  �� Text$ PutFile��5]      ��5\      �� $ SftpOperations�5]      5^      5_      5`      