   M S B A M L   `   `   `  ����O  KDynaMaya Installer, Version=1.1.100.4, Culture=neutral, PublicKeyToken=null#clr-namespace:DynaMayaDynaMaya  5      $    PluginPackInstaller.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   d `Microsoft.Expression.Controls, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35>ec5http://schemas.microsoft.com/expression/2010/controls  6�   %dynaMayaclr-namespace:DynaMaya  5        � Title$)  #CERVER Design Studio GC Plugin Pack��5   	   $��377.48��65   $	��700��6E    � 
ResizeMode$ CanMinimize=�6Q    � WindowStyle$
 None=�6j   
������� � Icon$ DynaMayaIcon.ico��6�    � 	Resources 5         hyperlinkStyle �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value��%A  &	        �� 5	   
   " ��6'   .�5
      ��  � ��$
 Bold"�6+   �� 5       � ��$ Eras ITC)�6+    5      P� 5      	 �@J�IsMouseOver
 . . �5      ��  � ��
 � ���63   5      P� 5      	 �@J�IsMouseOver
 .  . �5      ��  � ��
 �   �63   5      5      5      .��5      � $��	21,0,10,0q�6   .+�5   
   �� -   5        $
��Left=�5      $	��379��6+   $	��Top=�68   $	��696��6P   $"��Resources/pip_background.png��6\   $
��Fill=�6�   $��-18,0,-9,-2q�6�   �� 5   
   -   5         ��Name$ 	cerverorg��5      #���   6#   $��www.cerver.org��6L   $
��Left=�6e   $��	492,4,0,0q�6�   $	��Top=�6�   $	��121��6�   6�   �� 5   
   -   5         ��Name$ btClose��5      $��X��6"   $��Right=�6.   $	��Top=�6J   $��49.412��6b   $
��Bold"�6�   $��23.751��6�   $��0,0,0.683,0q�6�   $��16(�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6q   Z� ��6�   Z� 6�   �� 5   
   -   5        $ btNext��5      $��Next >>��6!   $��Right=�69   $��Bottom=�6U   $	��106��6p   $
��Bold"�6�   $��29��6�   $��0,0,10,0.333q�6�   $ 1.202,0.255&�6�   $��16(�6  ��6|   Z� ��6�   Z� 6  �� 5    
   -   5        $ btPrev��5       $��<< Back��6!   $��Right=�69   $��Bottom=�6U   $��97��6p   $
��Bold"�6�   $��29��6�   $��0,0,121,0.333q�6�   $ 1.202,0.255&�6�   $��16(�6  ��6{   Z� ��6�   Z� 6  �� 5!   
   -   5        $ btExit��5!      $��CLOSE��6!   $��Right=�61   $��Bottom=�6M   $��78.68��6h   $��29.488��6�   $��0,0,0.683,0q�6�   $ 1.202,0.255&�6�   $��16(�6  ��6v   Z� ��6�   Z� 6  �� 5"   
   -   5        $ 	cervernet��5"      #���   6#   $��www.cerver.net��6L   $
��Left=�6e   $��
492,20,0,0q�6�   $	��Top=�6�   $	��121��6�   6�   �� 5#   
   -   5         ��Name$ image��5#      $
��Left=�6   $��81��6:   $��	34,10,0,0q�6F   $	��Top=�6Y   $��81��6q   $#��Resources/DynaMayaIcon256.png��6|   �� 5$   
   -	   5        $ label��5$      $��DynaMaya��6   $
��Left=�62   $��	333,0,0,0q�6M   $	��Top=�6`   $ 0.5,0.5&�6x   $��35��6�   $	��138��6�   $��Eras ITC)�6�   $��24(�6�   $
��Bold"�6�      DynaMaya.scrWelcome  5%   
   -
   5          Name$ p0��5%         DynaMaya.scrLegal  5&   
   -   5          Name$ p1��5&         DynaMaya.scrFilePath  5'   
   -   5          Name$ 
PathScreen��5'         DynaMaya.scrInstalled  5(   
   -   5          Name$ scrInstalled��5(       $y�Hidden=�66   5*      5+      