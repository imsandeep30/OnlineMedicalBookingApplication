export class CartDto {
    cartId: number=0;
    userId: number=0;
    items: CartItemDTO[]=[];
    totalPrice: number=0;
}
export class CartItemDTO {
    mmedicneName : string = '';
    medicineId : number = 0;
    quantity : number = 0;
}
