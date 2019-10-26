# Custom DataGrid control for Xamarin Form
This code repository introduces a datagrid control for Xamarin Forms community. It comes with sorting capability. It is still under development.

## Result
<img align="center" src="https://github.com/abdoemad/XamF.Controls.DataGrid/blob/master/demo-result.png" width="400"/>

## Usage
```xml
<controls:DataGrid Grid.Row="1" ItemsSource="{Binding ProductList}" 
                   ItemTappedCommand="{Binding SelectedProductCommand}"
                   AutoSort="True" SortCommand="{Binding SortCommand}" 
                   HeaderBackgroundColor="LightBlue">
    <controls:DataGrid.Columns>
        <core:DataGridColumn Title="Name" Width=".4*" PropertyName="Name" SortingEnabled="True"></core:DataGridColumn>
        <core:DataGridColumn Title="Price" Width=".2*" PropertyName="Price" SortingEnabled="True"></core:DataGridColumn>
        <core:DataGridColumn Title="Stock" Width=".4*">
            <core:DataGridColumn.CellTemplate>
                <DataTemplate>
                    <ProgressBar x:Name="progressBar" Margin="2" WidthRequest="50" 
                                 Progress="{Binding Stock, Converter={StaticResource Key=progressToConverter}, ConverterParameter={Binding Source={x:Reference progressBar}}}" ProgressColor="LightGreen"></ProgressBar>
                </DataTemplate>
            </core:DataGridColumn.CellTemplate>
        </core:DataGridColumn>
    </controls:DataGrid.Columns>
    <controls:DataGrid.FooterPlaceHolder>
        <Label Text="{Binding ProductList.Count, StringFormat='Products count: {0}'}" BackgroundColor="LightBlue"/>
    </controls:DataGrid.FooterPlaceHolder>
</controls:DataGrid>
```
## Layout Design
<img align="center" src="https://github.com/abdoemad/XamF.Controls.DataGrid/blob/master/layout-design.png"/>

## Code Structure
<img align="center" src="https://github.com/abdoemad/XamF.Controls.DataGrid/blob/master/code-structure.PNG"/>

## TODO
- Add pager
- Add seraching capability 
- Enhance sorting
- Add bindable styles
- Add Empty Content template in case there is no rows
- Add loader
- Add alternative rows style
- Add unit testing
- Expose it as NuGet package
