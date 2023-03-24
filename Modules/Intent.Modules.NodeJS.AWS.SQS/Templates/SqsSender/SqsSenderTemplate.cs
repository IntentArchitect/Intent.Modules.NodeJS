using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.SQS.Templates.SqsSender
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class SqsSenderTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            // https://docs.aws.amazon.com/sdk-for-javascript/v3/developer-guide/sqs-examples-send-receive-messages.html#sqs-examples-send-receive-messages-sending
            return @"import  { SendMessageCommand, SQSClient, SendMessageCommandInput, SendMessageCommandOutput } from ""@aws-sdk/client-sqs"";

export interface SqsSenderCommandInput<TMessage> extends Omit<SendMessageCommandInput, 'QueueUrl' | 'MessageBody'> {
    MessageBody: TMessage
}

export class SqsSender<TMessages> {
    constructor(
        private readonly queueUrl: string
    ) { }

    async send(params: SqsSenderCommandInput<TMessages>): Promise<SendMessageCommandOutput> {
        var client = new SQSClient({});

        var command = new SendMessageCommand({
            ...params,
            MessageBody: JSON.stringify(params.MessageBody),
            QueueUrl: this.queueUrl
        });

        const output = await client.send(command);
        return output;
    }
}";
        }
    }
}