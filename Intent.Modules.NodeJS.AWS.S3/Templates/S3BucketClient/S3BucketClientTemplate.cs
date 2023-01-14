using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.S3.Templates.S3BucketClient
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class S3BucketClientTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            // https://docs.aws.amazon.com/sdk-for-javascript/v3/developer-guide/s3-example-creating-buckets.html
            return $@"import {{
    GetObjectCommand,
    GetObjectCommandInput,
    GetObjectCommandOutput,
    ListObjectsCommand,
    ListObjectsCommandInput,
    ListObjectsCommandOutput,
    PutObjectCommand,
    PutObjectCommandInput,
    PutObjectCommandOutput,
    S3Client,
    UploadPartCommand,
    UploadPartCommandInput,
    UploadPartCommandOutput,
}} from '@aws-sdk/client-s3';
import {{ HttpHandlerOptions }} from '@aws-sdk/types';

export interface BucketGetObjectCommandInput extends Omit<GetObjectCommandInput, 'Bucket'> {{ }}
export interface BucketListObjectsCommandInput extends Omit<ListObjectsCommandInput, 'Bucket'> {{ }}
export interface BucketPutObjectCommandInput extends Omit<PutObjectCommandInput, 'Bucket'> {{ }}
export interface BucketUploadPartCommandInput extends Omit<UploadPartCommandInput, 'Bucket'> {{ }}

export class {ClassName} {{
    private readonly client: S3Client;

    constructor(
        private readonly bucketName: string
    ) {{
        this.client = new S3Client({{ }});
    }}

    async getObject(commandInput: BucketGetObjectCommandInput, options?: HttpHandlerOptions): Promise<GetObjectCommandOutput> {{
        return await this.client.send(new GetObjectCommand({{
            ...commandInput,
            Bucket: this.bucketName
        }}), options);
    }}

    async listObject(commandInput: BucketListObjectsCommandInput, options?: HttpHandlerOptions): Promise<ListObjectsCommandOutput> {{
        return await this.client.send(new ListObjectsCommand({{
            ...commandInput,
            Bucket: this.bucketName
        }}), options);
    }}

    async putObject(commandInput: BucketPutObjectCommandInput, options?: HttpHandlerOptions): Promise<PutObjectCommandOutput> {{
        return await this.client.send(new PutObjectCommand({{
            ...commandInput,
            Bucket: this.bucketName
        }}), options);
    }}

    async uploadPart(commandInput: BucketUploadPartCommandInput, options?: HttpHandlerOptions): Promise<UploadPartCommandOutput> {{
        return await this.client.send(new UploadPartCommand({{
            ...commandInput,
            Bucket: this.bucketName
        }}), options);
    }}
}}
";
        }
    }
}