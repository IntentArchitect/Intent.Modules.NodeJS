import { TptDerivedClassForAbstract } from './tpt-derived-class-for-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_derived_class_for_abstract_associated')
export class TptDerivedClassForAbstractAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  derivedClassForAbstractId: string;

  @ManyToOne(() => TptDerivedClassForAbstract, { cascade: ['insert', 'update'], nullable: false })
  derivedClassForAbstract: TptDerivedClassForAbstract;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}