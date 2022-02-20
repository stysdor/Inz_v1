import { MatPaginatorIntl } from "@angular/material/paginator";


export class CustomPaginator extends MatPaginatorIntl {
  itemsPerPageLabel = 'Rozmiar';
  nextPageLabel     = '';
  previousPageLabel = '';
}
