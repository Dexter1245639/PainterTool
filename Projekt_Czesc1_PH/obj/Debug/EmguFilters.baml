   M S B A M L   `   `   `  ����L  HProjekt_Czesc1_PH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Projekt_Czesc1_PHProjekt_Czesc1_PH  5   	   #    Projekt_Czesc1_PH.EmguFilters   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Projekt_Czesc1_PH     5   	     � Title$  Filtry Podstawowe��5   	   $	��600��6#   $	��900��60   .��5	      � .+�5
   
   �� -   5        $��imageSurface��5
      $	��500��6%   $	��500��61   $��70,17,330,17q�6>   ��5      �� 5       �� Color$ Black��6"    �� Opacity$ 1Q�60   .+�5      �� 5      �� 5   
   -   5         ��Name$ 	loadImage��5      $��Wczytaj obraz��6$   $
��Left=�6<   $��724,117,0,0q�6W   $	��Top=�6l   $��91��6�   6�   �� 5   
   -   5        $ 
resetImage��5      $��Resetuj obraz��6%   $
��Left=�6=   $��724,152,0,0q�6X   $	��Top=�6m   $��91��6�   6�   �� 5   
   -   5        $ sobelFilter��5      $��Sobel��6&   $
��Left=�66   $��724,278,0,0q�6Q   $	��Top=�6f   $��91��6~   6�   �� 5   
   $��Filtry:��6   $
��Left=�6"   $��750,235,0,0q�6=   $	��Top=�6R   �� 5   
   -   5        $ GuassianBlur��5      $��Rozmycie Gauusa��6'   $
��Left=�6A   $��714,320,0,0q�6\   $	��Top=�6q   $	��110��6�   6�   �� 5   
   -   5        $ 	CannyEdge��5      $��Krawędzie Canny��6$   $
��Left=�6>   $��714,362,0,0q�6Y   $	��Top=�6n   $	��110��6�   6�   �� 5   
   -   5        $ 
BlackWhite��5      $��Czarno-białe��6%   $
��Left=�6<   $��714,403,0,0q�6W   $	��Top=�6l   $	��110��6�   6�   �� 5   
   -   5        $ ColorSharpness��5      $��Wyostrzenie kolorów��6)   $
��Left=�6G   $��706,445,0,0q�6b   $	��Top=�6w   $	��127��6�   6�   5      5      