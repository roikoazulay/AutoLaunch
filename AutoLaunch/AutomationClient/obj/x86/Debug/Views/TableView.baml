   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   &     AutomationClient.Views.TableView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	     � Title$  Table Action��5   	   $	��399��5   	   $	��280��5   	    � Icon$) #/Images/Mimetype-vcalendar-icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $	��247��6   
�������.+�5   
   c� $��41��6   .@�5      �� -   5        $��saveBtn��5      ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6*   �� *GetToolBoxList.Count+ �� Value 06S   .a�5   "   ��  �� Property � {� �� Value .  6H   5      5      .��5      �� $��25��6   $��/Images/Pen-Blue-icon.png��6$   5      5      � 5   
   $	��200��6    � ColumnDefinitions 5      �� 5      $	��97* �6#   �� 5      $
��271* �6#   	 � RowDefinitions	 5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      .+�5'      �� $��0��6   $��Center=�5(      $��Table Name:��5)      �� 5*      -   5        $��tableNameCmb��5*      $��1��5+      $	��250��5,      $
��Left=�5-      
 �� 
IsEditable
 . ��5/      ��  �� Path$ VariableList��� 51      $��1��6   $��Center=�52      $��Value:��53      �� 54      -   5        $��valueCmb��54      $��1��55      $��1��56      $	��250��57      $
��Left=�58      
 . ��5:      �� $ VariableList��� 5;      $��2��6   $��Center=�5<      $
��Row:��5=      �� 5>      -   5        $��rowCmb��5>      $��2��5?      $��1��5@      $	��250��5A      $
��Left=�5B      
 . ��5D      �� $ VariableList��� 5F      $��3��6   $��Center=�5G      $��Column:��5H      �� 5I      -   5        $��colCmb��5I      $��3��5J      $��1��5K      $	��250��5L      $
��Left=�5M      
 . ��5O      �� $ VariableList��� 5P      $��4��6   $��Center=�5Q      $��
File Name:��5R      �� 5S      -   5        $��fileCmb��5S      $��4��5T      $��1��5U      $	��250��5V      $
��Left=�5W      
 . ��5Y      �� $ VariableList��� 5Z      $��5��6   $��Center=�5[      $��Target Var:��5\      �� 5]      -   5        $��	targetCmb��5]      $��5��5^      $��1��5_      $	��250��5`      $
��Left=�5a      
 . ��5c      �� $ VariableList��� 5e      $��6��6   $��Center=�5f      $��Action:��5g      �� 5h      -   5        $��operationCmb��5h      $��6��5i      $��1��5j      $	��250��5k      $
��Left=�5l      
 .  �� Text$ CreateTable��5o      ��5n      �� $ TableOperations�5o      5p      5q      5r      