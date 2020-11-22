export class CartLine {
    public ProductId: number;
    public Count: number;

    constructor(productId: number, count: number) {
        this.ProductId = productId;
        this.Count = count;
    }
}
