   M S B A M L   `   `   `  ����   AutomationClient    AutomationClient.AboutWin   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     
idAboutBox��  � Icon$>  8Images\Apps-preferences-desktop-launch-feedback-icon.png��   AboutDialogStyle#���    � WindowStartupLocation$ CenterOwner=� � Title ��  �� RelativeSource �� *Self+ �� Mode$ OneTime=� �� Path$ ProductTitle� � 	Resources   	aboutLogo �� 
TargetType �� Property	 �� Value  DisplayAreaStyle  BackgroundStyle
 �� EndPoint � Offset � Color  OkButtonStyle��%�  &	       &	  D     &	 #    &	 N    '�� �    &	     �� $>��8Images\Apps-preferences-desktop-launch-feedback-icon.png���� " �.���  � ��$
	 Auto����  � ��$		 300����  �@�ShowInTaskbar	 .  ��  �@�
ResizeMode$	 NoResize=���  �@�WindowStyle$	 SingleBorderWindow=���  �@�SizeToContent$	 Height=��� " ��.���  � ��$	 
10,10,10,5q��� " ��.���  � ��	 �� $	
 0,1&�.��� $ 0Q�$ 	LightBlue��� $
 0.55Q�$ White��� $
 0.85Q�$ White��� $ 1Q�$ 	LightBlue���� " ��.���  � ��$	 0q��� " ��.���  � ��$	 75����  � ��$	 0q���  � ��$
	 Left=���  �@��	IsDefault	 . ��  �@��IsCancel	 . .���� 
clientArea��#��[ .+���  �� ResourceKey$ 	aboutLogo���� displayArea��#��[  � DataContext ��  �� *FindAncestor+ �� AncestorType" �$ OneTime=�.+��� -    ��Name$ productName��productName������ $ OneTime=�$ Product��� versionArea�� �� Orientation$ 
Horizontal=�.+��� -   $ versionLabel��VersionLabel��$��
Version - ���� -   $ version��version������ $ OneTime=�$ Version��� -   $ 	copyright��	copyright������ $ OneTime=�$ 	Copyright��� -   $ reserved��reserved��$��All Rights Reserved.��]� 
buttonArea��.+��� -    ��Name$ okButton��okButton��#��[ $��OK��