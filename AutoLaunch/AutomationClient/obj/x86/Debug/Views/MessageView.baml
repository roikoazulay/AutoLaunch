   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   (    "AutomationClient.Views.MessageView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Message Show��5   	   $	��403��5   	   $	��135��5   	    � Icon$  /Images/Text-File-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $��98��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $��55��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5"      .+�5$      �� $��0��6   $��Center=�5%      $��Message:��5&      �� 5'      -   5        $��
messageCmb��5'      $��1��5(      $	��250��5)      $
��Left=�5*      
 �� 
IsEditable
 . ��5,      ��  �� Path$ VariableList��� 5-      $��1��6   $��TimeOut:��6!   �� 5.      -   5        $��
timeOutCmb��5.      $��1��5/      $��1��50      $	��250��51      $
��Left=�52      
 . ��54      �� $ VariableList�55      56      57      