import React from "react";
import Nav from "./Nav";
import Header from "../output/Header";

type PageTemplateProps = {
    headerValue: string,
    children: React.ReactNode
}

const PageTemplate = ({headerValue, children}: PageTemplateProps) => {
    return (
        <>
            <Nav/>
            <Header value={headerValue}/>
            {children}
        </>
    )
}

export default PageTemplate;