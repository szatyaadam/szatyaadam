   M S B A M L   `   `   `  ���� G  CCookBook.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CookBook.WPF.ViewModelsCookBook.WPF.ViewModels      CookBook.WPF.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4vm%clr-namespace:CookBook.WPF.ViewModels     -   $	��450��$	��800��$	��600��$	��800��  � WindowStyle$
  None=�
������  � 
ResizeMode$ CanResizeWithGrip=� � DataContext +   %CookBook.WPF.ViewModels.MainViewModel   � 	Resources  �� 
TargetType �� Property �� Value��%E   'm�       �� " m�.���  � ��$ 5q���  � ��$ Right=�.���� $��10 40 10 10q� �� CornerRadius$ 5,5,0,5������  �� EndPoint$
 1, 0&�	 �� 
StartPoint$		 0,1&�.��� 
 � Color$
 	#FF3E3734�� � Offset$ 0Q�� $
 	#FF54230C��$ 1Q�� $
 	#FFF9C896��$ 0.444Q����� $	 0,1&�$		 1,0&� �� Opacity$
 0.85Q�.��� $
 	#FF3E3734��$ 0Q�� $
 	#FF54230C��$ 1Q�� $
 	#FFF9C896��$ 0.444Q�._��  � ShowGridLines$ False�� � RowDefinitions �� $��0 ��� $��40 �$��5��$��65���� $��6 ��� $��* � � ColumnDefinitions �� $��* �.+�o� $��1�����	#FF976E4E$��Stretch=����� .R�$�  $� Orientation$ 
Horizontal=����� $	 0.5,0&�$ 0.5,1&�.��� $
 #323232��$	 0.0Q�� $
 #3F3F3F��$
 0.45Q�� $
 #1C1C1C��$	 1.0Q�.@�m� $��50��$��3,3,3,3q�   NoMouseOverButtonTemplate#��[  ����  �� ImageSource$ /Views/Icon.png�� j� Stretch$ UniformToFill=�m� -   $
��Bold"�$��20(�
�������$��Ételek�� m� CommandParameter$ Meals�� m� Command �� *UpdateViewCommand+m� -   $
��Bold"�$��20(�
�������$��Felhasználók��$
 User�� �� *UpdateViewCommand+m� -    m�Name$ Login��$
��Bold"�$��20(�
�������$��Bejelentkezés��$ Login�� �� *UpdateViewCommand+m� -   $ tbTime��$��Right=�$
��Bold"�$��20(�
�������$�� ��$
 Time���� $��2��
���   �$��Stretch=��� $��3������ *SelectedObject+]� $��0��$��	290,0,0,0q�$	��290��$��1��$��Right=�.+��� -    ��Name$ ExitBtn��$��X��$��30��$��2,0,0,0q�$��40��$��DemiBold"�$��Right=� J� RenderTransformOrigin$ 0.5,0.5&�$��Right=����	#FFC8C8C8$��
0,0,0,-100q�����  �� Color$ 	#FFB12C2C��$ 1Q�z�V� .~�T�  T� Y$	 -70Q��� -   $ SizeBtn��$��#��$��30�����	#FFC0AA19$��2,0,0,0q�$��40��$��DemiBold"����	#FFC8C8C8$��Right=�$ 0.5,0.5&�$��Right=�$��
0,0,0,-100q�z�V� .~�T� $	 -70Q��� -   $ 
DesktopBtn��$��_��$��30��$��40��$��DemiBold"����	#FF1FC635$ 0.5,0.5&�$��0,0,0,0q�$��Right=�$��Right=�$��
0,0,0,-100q�z�V� .~�T� $	 -70Q�