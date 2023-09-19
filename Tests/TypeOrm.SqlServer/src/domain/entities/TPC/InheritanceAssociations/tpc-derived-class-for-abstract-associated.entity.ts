import { TpcDerivedClassForAbstract } from './tpc-derived-class-for-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpc_derived_class_for_abstract_associated')
export class TpcDerivedClassForAbstractAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  derivedClassForAbstractId: string;

  @ManyToOne(() => TpcDerivedClassForAbstract, { cascade: ['insert', 'update'], nullable: false })
  derivedClassForAbstract: TpcDerivedClassForAbstract;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}