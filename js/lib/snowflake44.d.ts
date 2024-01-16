export declare class Uid64 {
    private static _PreviousTimeFrame;
    private static _RandomsOfCurrentTimeFrame;
    static generateUid(): bigint;
    private static encodeDateTime;
    static decodeDateTime(uid64: bigint): Date;
}
//# sourceMappingURL=uid64.d.ts.map