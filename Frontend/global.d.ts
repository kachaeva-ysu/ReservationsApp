type ClassNames = Record<string, string>;
declare module '*.css' {
    const classNames: ClassNames;
    export = classNames;
}