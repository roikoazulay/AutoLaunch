   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   &     AutomationClient.Views.EmailView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Email��5   	   $	��375��5   	   $	��286��5   	    � Icon$! /Images/email-send-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� 
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��207��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      .+�5)      �� $��0��6   $
��Left=�5*      $��
Recipient:��5+      �� 5,      -   5        $��recipientTxb��5,      $��0��5-      $��1��5.      $	��250��5/      $
��Left=�50      $��Recipient@Auto.com��51      �� 53      $��1��6   $
��Left=�54      $��From:��55      �� 56      -   5        $��fromTxb��56      $��1��57      $��1��58      $	��250��59      $
��Left=�5:      $��AutomationServer@Auto.com��5;      �� 5=      $��2��6   $
��Left=�5>      $��Subject:��5?      �� 5@      -   5        $��
subjectTxb��5@      $��2��5A      $��1��5B      $	��250��5C      $
��Left=�5D      $$��Message from Automation Server��5E      �� 5F      $��3��6   $
��Left=�5G      $��Body:��5H      �� 5I      -   5        $��bodyTxb��5I      $��3��5J      $��2��5K      $��1��5L      $	��250��5M      $
��Left=�5N      $��Some Message Data��5O      �� 5Q      $��5��6   $
��Left=�5R      $��Mail Server:��5S      �� 5T      -   5        $��
mailSrvTxb��5T      $��5��5U      $��1��5V      $	��250��5W      $
��Left=�5X      $��exc2010.orpak.corp��5Y      �� 5Z      $��6��6   $
��Left=�5[      $��Action:��5\      �� 5]      -   5        $��operationCmb��5]      $��6��5^      $��1��5_      $	��250��5`      $
��Left=�5a      
 �� 
IsEditable
 .  �� 
IsReadOnly .  �� Text$
 Send��5d      5e      5f      5g      