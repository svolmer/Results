# 📄 Code Documentation
Generate well-formed XML documentation comments for all publicly visible types and their public members:
- **Always** document types or members with
  - `<summary>` for a short overall description
  - `<remarks>` for supplemental information

- **Always** document member declarations with
  - `<returns>` for a description of return values
  - `<param>` for a description of parameters
  - `<paramref>` for text referencing parameters  
  - `<exception>` for exceptions that can be thrown
  - `<value>` for a description of value representations

- **Always** document generic types and methods with
  - `<typeparam>` for a desciption of generic type parameters
  - `<typeparamref>` for text referencing generic type parameters
    
- Add structure to text with 
  - `<para>` for paragraphs
  - `<list>` for lists or tables
  - `<c>` for verbatim code in code-like format 
  - `<code>` for multiline source code in code-like format
  - `<example>` for examples

- Eliminate duplicate text with 
  - `<inheritdoc>` for common documentation to be inherited from base types or members
  - `<include>` for including XML from an external file
  
- Specify links to reference to additional information with
  - `<see>` for links
  - `<seealso>` for alternative links
  - `<cref>` for references to code 
  - `<href>` for referencs to web pages 

- Format the XML elements with 
    - an indentation of 4 spaces per level
    - a maximum content text width of 120 characters per line
    - `<summary>` being the first element in the XML comment
    - `<remarks>` being the last element in the XML comment
