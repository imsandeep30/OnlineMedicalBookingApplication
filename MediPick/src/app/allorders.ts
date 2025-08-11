export interface Allorders {
    orderId: number;
    userId: number;
    orderDate: Date;
    shippingAddress: string;
    paymentStatus: string;
    orderStatus: string;
    totalAmount: number;
}
