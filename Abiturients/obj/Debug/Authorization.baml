   M S B A M L   `   `   `  ����F  BAbiturients, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AbiturientsAbiturients  5   	       Abiturients.Authorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Abiturients     5   	     � Title$  Authorization��5   	   $	��240��6   $	��480��6,    � WindowStyle$ 
ToolWindow=�68   .��5	      � -   5         �Name$ MainRoot��5	      
������� � ColumnDefinitions 5
   
   �� 5       � RowDefinitions 5   
   �� 5      $	��55* �6   �� 5      $	��55* �6   �� 5      $	��55* �6   �� 5      $	��52* �6   �� 5      $��3* �6   .+�5   
   �� -   5        $��Вхід��5      $��Center=�6    $��Center=�6=   $��40��6X   $	��196��6d   $��3��6�   ���	#FF5EAC61$
��Bold"�6�   $��20(�6�    �� 	IsDefault . 6�   �� 5   
   $��Авторизація��6   $��Center=�6&   $��55��6C   $��Center=�6O   $	��480��6j   $��Center=�6v   $��Center=�6�   $��Arial)�6�   $��36(�6�   $
��Bold"�6�   ���	#FF47804A6�   �� 5   
   -   5         ��Name$ 	login_box��5      $
��Left=�6%   $��35��6@   $��	227,0,0,0q�6L   $��1��6_   $�� ��6l    �� TextWrapping$
 Wrap=�6t   $��Center=�6�   $	��232��6�    J� RenderTransformOrigin$ 0.503,0.525&�6�   $��Center=�6�   $��Center=�6�   G� 5   
   -   5        	 G�Name$	 password_box��5      $
��Left=�6,   $��35��6G   $��	227,0,0,0q�6S   $��2��6f   $��Center=�6s   $	��232��6�   $ 0.503,0.525&�6�   $��Center=�6�   $��Center=�6�   �� 5   
   $
��Left=�6   $��18��6/   $��88,0,0,0q�6;   $��1��6M   $��
Логін��6Z   $
��Wrap=�6g   $��Center=�6{   $��46��6�   $��Arial)�6�   $
��Bold"�6�   $��Italic$�6�   �� 5   
   $
��Left=�6   $��22��6/   $��	83,12,0,0q�6;   $��2��6N   $��Пароль��6[   $
��Wrap=�6i   $	��Top=�6}   $��59��6�   $��Arial)�6�   $
��Bold"�6�   $��Italic$�6�   $ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5      
 �� Angle$
 0.158Q�6&   T� 5       5!      5#      5$      5%      