   M S B A M L   `   `   `  ���� K  GAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  )    #AutomationClient.Views.VariableView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0localclr-namespace:AutomationClient       � Icon$$  /Images/numeric_field_icon.jpg�� � Title$ 	Variables��$	��294��$	��469��.���� 
�������.+�c� $��41��.@��� -   $��saveBtn������  �� Triggers ��  �� Binding �� *GetToolBoxList.Count+ �� Value 0.a���  �� Property � {� �� Value .  .���� -   $��25��$��/Images/Pen-Blue-icon.png����  �� Orientation$ 
Horizontal=�.+��� $��Varible Name:���� -   $��varCmb�� �� 
IsEditable . $	��200��$��2,0,0,0q����� 	 �� Path$	 VariableList��� $ 
Horizontal=�$��0,2,0,0q�.+��� $��Value:���� -   $��valueTxb��$	��356��$��44,0,0,0q�$��51��
 �� TextWrapping$

 Wrap=�" �� VerticalScrollBarVisibility$ Visible=� �� AcceptsReturn . �� $ 
Horizontal=�$��0,2,0,0q�.+��� $��Action:���� -   $��operationCmb�� �� 
IsReadOnly .  .  �� Text$ Create��$	��200��$��40,0,0,0q����� $	 VariableOperations��� $ 
Horizontal=�$��0,2,0,0q�.+��� $��Action Value:���� -   $��actionValueTxb��$	��356��$��7,0,0,0q�$��51��$

 Wrap=�$ Visible=� . �� $ 
Horizontal=�$��0,2,0,0q�.+��� $��Target Varible:���� -   $��targetVarCmb�� . $	��200������ $	 VariableList��� $ 
Horizontal=�$��0,2,0,0q�.+��� $��Target File:���� -   $��targetFileTxb��$	��356��$��19,0,0,0q�