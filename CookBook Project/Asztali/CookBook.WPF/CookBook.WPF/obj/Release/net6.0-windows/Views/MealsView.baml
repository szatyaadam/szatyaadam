   M S B A M L   `   `   `  ���� G  CCookBook.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CookBook.WPF.ValidatorsCookBook.WPF.Validators  "    CookBook.WPF.Views.MealsView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3v%clr-namespace:CookBook.WPF.Validators     
�������.��� $��5q�  � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��24(�$��Center=�$��0��.��	Ételek9� $��1��$��1��$%��Új Mértékegység/ÉtelTípus��.���   �� $
��auto ��� $
��auto ��� $
��auto � � ColumnDefinitions �� $	��130���� �� �� �� $	��150���� $	��130��.+��� $��0��$��5��$��Mértékegység törlés������ *DeleteItemCommand+�� $��0��$��0��$��Új mértékegység������ *NewItemCommand+�� -    ��Name$ MeasureName��$��0��$��1������ *NewMeasure.Measure+.���� -    ��Name$ ChangeMeasure��$��0��$��2�� �� DisplayMemberPath$ Measure������ *	Measure+ �� SelectedItem �� *ChangeMeasure+ �� SelectedValue �� *ChangeMeasure+�� $��0��$��3������ *ChangeMeasure.Measure+.���� $��0��$��4��$#��Mértékegység változtatás��$
��auto������ *ChangeItemCommand+�� $��1��$��5��$��Ételtípus törlés������ *DeleteItemCommand+�� $��1��$�� 0��$��Új ételtípus������ *NewItemCommand+�� $��1��$��1������ *NewMealType.MealTypeName+.���� -   $ MealtypeChange��$��1��$��2��$ MealTypeName������ *
mealType+ �� *ChangeMealType+ �� *ChangeMealType+�� $��1��$��3������ *ChangeMealType.MealTypeName+.���� $��1��$��4��$��Ételtípus változtatás��$
��auto������ *ChangeItemCommand+9� -   $	��Add��$��2��$��0��$��Recept Részletei��.��� -   $��grid1��  �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+��� $
��Név��$��0��$��0���� -   $ txtMealName��$��0��$��1��$	��200��   validationTemplate =� ErrorTemplate# [  .��.����  �� Path$ SelectedMeal.MealName�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� ValidatesOnDataErrors$

 True�� �� ValidationRules +   %CookBook.WPF.Validators.NameValidator  �� $	��Ár��$��0��$��2���� -   $ txtPrice��$��0��$��3��$	��235��$
��Left=�# [  .��.���� $ SelectedMeal.Price�$	 PropertyChanged=�$

 True�� ,   &CookBook.WPF.Validators.PriceValidator  �� $��Elkésztési Idő��$��1��$��2���� -   $ txtTime��$��1��$��3��$	��235��$
��Left=�# [  .��.���� $" SelectedMeal.PreperationTime�$	 PropertyChanged=�$

 True�� +   %CookBook.WPF.Validators.TimeValidator  �� $��ÉtelTípus��$��1��$��0���� -	   $ 	Mealtypes��$��1��$��1��$ MealTypeName�� �� SelectedValuePath$ Id������ *
mealType+ �� *SelectedMeal.Mealtype+ �� *SelectedMeal.Mealtype.Id+�� $��Leírás��$��2��$��0���� -
   $��
Discripton��$��2��$��1��$	��520��$��3��$��2��$
��Left=����� *SelectedMeal.Discription+�� -   $��NewIngButton��$��Új hozzávaló��$��3��$��0��$	��100��$
��Left=��� -    ��Name$ OldIngButton��$��Meglévő hozzávaló��$��3��$��0��$
��auto��$
��Left=�$y�	Collapsed=��� $��Hozzávaló��$��4��$��0���� -   $ newIngredientName��$��5��$��3��$
��Left=�$��40��$��70��$y�	Collapsed=�$��
-120,0,0,0q����� *NewMaterial.IngredientName+�� -   $ newQuantity��$��5��$��1��$
��Left=�$��40��$y�	Collapsed=����� *NewIngredient.Quantity+.���� -   $ chooseNewIngredient��$��4��$��2��$ Measure��$ Id��$
��Left=�$��85�� J� RenderTransformOrigin$	 1,1&� K� ZIndex$ -1�����	#FFF9F6F6$��35,0,0,0q�$y�	Collapsed=����� *	Measure+ �� *SelectedMeasure+ �� *SelectedMeasure.Id+z�V� .~�T�  T� X$	 -35Q��� -   $ 
ingredient��$��4��$��1��$ Materials.IngredientName��$	��100��$
��Left=����� *SelectedMeal.Ingredients+�� $��2��$��5��$
��Left=�$��
-100,0,0,0q�$��Hozzávaló név:���� $��4��$��3��$��Hozzáadás������ *AddItemCommand+���� *SelectedMeal+�� $��5��$��3��$��Eltávolítás������ *RemoveItemCommand+����  �� ElementName$ 
ingredient��$ SelectedItem��� -   $ IngredientName��$��5��$��3��$
��Left=�$��40��$��
-120,0,0,0q����� $ 
ingredient��$+ %SelectedItem.Materials.IngredientName��� $��0��$��5��$��Right=�$��Mennyiség:���� -   $ Quantity��$��5��$��1��$��40��$
��Left=�# [  .��.���� $ 
ingredient��$ SelectedItem.Quantity�$	 PropertyChanged=�$

 True�� /   )CookBook.WPF.Validators.QuantityValidator  �� $��2��$��4��$��Mértékegység:��$��
-100,0,0,0q�$
��Left=��� -   $ Measure��$��4��$��2��$��70��$
��Left=� �� 
IsReadOnly$
 True������ $ 
ingredient��$2 ,SelectedItem.Materials.UnitOfMeasure.Measure��� -   $ chooseIngredient��$��4��$��2��$ Measure��$ Id��$
��Left=�$��85��$	 1,1&�$ -1�����	#FFF9F6F6$��35,0,0,0q����� *	Measure+ �� $ 
ingredient��$* $SelectedItem.Materials.UnitOfMeasure� �� *)'SelectedItem.Materials.UnitOfMeasure.Id+z�V� .~�T� $	 -35Q��� $	��Új��$��0��$��4������ *
NewCommand+����  �� 
TargetType" �� �� BasedOn# [B��.���  �� Property � {� �� Value$ false�� �� Triggers ^�  ^� 
Conditions ��  �� Binding �� $ txtPrice��$ (Validation.HasError)� �� Value	 false��  �� $ txtTime��$ (Validation.HasError)�	 false��  �� $ txtMealName��$ (Validation.HasError)�	 false.0���  � {�$
 true���� -   $��Mentés��$��1��$��4������ *SaveCommandAsync+���� *SelectedMeal+���� " ��# [B��.���  � {�$ false�� ^�  ��  �� $ txtPrice��$ (Validation.HasError)�	 false��  �� $ txtTime��$ (Validation.HasError)�	 false��  �� $ txtMealName��$ (Validation.HasError)�	 false.0���  � {�$
 true���� $��Törlés��$��2��$��4������ *DeleteCommandAsync+���� *SelectedMeal+� $��3�� �� �� $	��78* �.+���  �� Orientation$ 
Horizontal=�$��Center=�$��2��.+��� $��Összesen: ���� ���� *
TotalItems+�� $�� elem, per oldal: ���� $	��100������ *ItemsPerPageList+ �� *ItemsPerPage+�� $��	456,5,0,5q�$ 
Horizontal=�$
��Left=�$��1��.+��� $	��200��$��Center=����� *	SearchKey+�� $��Keresés������ *LoadDataCommand+&   System.Windows.Controls.DataGrid  -   $��4��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    AutoGenerateColumns$ False��   CanUserAddRows$ False��   CanUserDeleteRows$ False��    SelectionMode$  Single=�!   
IsReadOnly$
! True������ *Meals+ �� *SelectedMeal+"   Columns" 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn#   Header$
# Név��1	  +System.Windows.Controls.DataGridBoundColumn$ 	  Binding$ �� *
MealName+  $# Elkészítési Idő��$ �� *PreperationTime+  $	# Ár��$ �� *Price+  $# Ételtípus��$ �� *Mealtype.MealTypeName+  $# Leírás��$ �� *Discription+  $# Hozzávaló��$ �� *)'Ingredients[0].Materials.IngredientName+% �� StringFormat$% {0}...��� $��5��$��5q� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��<<��$��0������ *FirstPageCommand+�� $��<��$��1������ *PreviousPageCommand+�� $��2��$��Center=����� *CurrentPage+�� $��>��$��3������ *NextPageCommand+�� $��>>��$��4������ *LastPageCommand+