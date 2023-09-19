import { TptAbstractBaseClass } from './tpt-abstract-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_abstract_base_class_associated')
export class TptAbstractBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  abstractBaseClassId: string;

  @ManyToOne(() => TptAbstractBaseClass, { cascade: ['insert', 'update'], nullable: false })
  abstractBaseClass: TptAbstractBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}