### Version 4.0.0

- Improvement: Updated dependencies to use separate `Intent.Code.Weaving.TypeScript` module.
- Fixed: `ApiOkResponse` has a `type` argument that wasn't correctly set with primitive types.

### Version 3.4.3

- Fixed: When controller operations returned a string an error would show in the Swagger UI.

### Version 3.4.2

- Fixed: Header field was not adequately bound.

### Version 3.4.1

- Updated dependencies and supported client versions to prevent warnings when used with Intent Architect 4.x.

### Version 3.3.6

- It's now possible to have DTOs inherit from other DTOs.
- It's now possible to apply `Is Abstract` to DTOs.
- Fixed: Generic type parameters would not be generated for DTOs.

### Version 3.3.5

- Update: Updated Intent.Metadata.WebApi that will no longer automatically apply HttpSettings stereotypes but will auto add them using event scripts.

### Version 3.3.4

- New: Http Settings' Return Type Mediatype setting will determine if the primitive return type should be wrapped in a JsonResponse object or not.
- Fixed: DTO Model generation had possible circular reference issue which would crash the Software Factory during execution.
